using System.Security.Cryptography;
using System.Text;

namespace MyProject.Data.Encryption
{
    public class AES
    {
        // https://stackoverflow.com/a/18502617/2860309

        /*  Wanting to stay compatible with NodeJS
            *  http://stackoverflow.com/questions/18502375/aes256-encryption-decryption-in-both-nodejs-and-c-sharp-net/
            *  http://stackoverflow.com/questions/12261540/decrypting-aes256-encrypted-data-in-net-from-node-js-how-to-obtain-iv-and-key
            *  http://stackoverflow.com/questions/8008253/c-sharp-version-of-openssl-evp-bytestokey-method
            *
            * // default iv is 0000000000000000 for simple-free-encryption-tool
            * var cipher = crypto.createCipheriv('aes-256-cbc', 'secret key', '0000000000000000');
            * var encrypted = cipher.update("test", 'utf8', 'base64') + cipher.final('base64');
            * 
            * // default iv is 0000000000000000 for simple-free-encryption-tool
            * var decipher = crypto.createDecipheriv('aes-256-cbc', 'secret key', '0000000000000000');
            * var plain = decipher.update(encrypted, 'base64', 'utf8') + decipher.final('utf8');
            */

        public const string NULL_IV = "0000000000000000";

        public static string Encrypt(string input, string key, string iv = NULL_IV)
        {
            // simple-free-encryption-tool hashes key to ensure it's of the correct length
            key = MD5Hasher.Hash(key);

            return Convert.ToBase64String(EncryptStringToBytes(
                input,
                RawBytesFromString(key),
                RawBytesFromString(iv)
            ));
        }

        public static string Decrypt(string inputBase64, string key, string iv = NULL_IV)
        {
            // simple-free-encryption-tool hashes key to ensure it's of the correct length
            key = MD5Hasher.Hash(key);

            return DecryptStringFromBytes(
                Convert.FromBase64String(inputBase64),
                RawBytesFromString(key),
                RawBytesFromString(iv)
            );
        }

        private static byte[] RawBytesFromString(string input)
        {
            var ret = new List<Byte>();

            foreach (char x in input)
            {
                var c = (byte)((ulong)x & 0xFF);
                ret.Add(c);
            }

            return ret.ToArray();
        }

        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged cipher = new RijndaelManaged())
            {
                cipher.Key = Key;
                cipher.IV = IV;
                //cipher.Mode = CipherMode.CBC;
                //cipher.Padding = PaddingMode.PKCS7;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = cipher.CreateEncryptor(cipher.Key, cipher.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream. 
            return encrypted;
        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (var cipher = new RijndaelManaged())
            {
                cipher.Key = Key;
                cipher.IV = IV;
                //cipher.Mode = CipherMode.CBC;
                //cipher.Padding = PaddingMode.PKCS7;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = cipher.CreateDecryptor(cipher.Key, cipher.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }

    public class MD5Hasher
    {
        // adapted from https://stackoverflow.com/a/24031467/2860309
        public static string Hash(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }
    }
}