namespace Application.Observability
{
    public class LogLocal
    {
        public void CriaLogLocal(string mensagem, bool status)
        {
            string arquivoLog = "log.txt";

            string entradaLog = $"{DateTime.Now} - {mensagem} - {(status ? "Sucesso" : "Falha")}";

            try
            {
                using (StreamWriter writer = File.AppendText(arquivoLog))
                {
                    writer.WriteLine(entradaLog);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao registrar o log: {ex.Message}");
            }
        }
    }
}
