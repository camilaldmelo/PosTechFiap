﻿namespace Application.ViewModel
{
    public class ClienteViewModel
    {    
        public int IdCliente { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }        
        public string? Email { get; set; }
        
        public DateTime Data { get; set; }
    }
}
