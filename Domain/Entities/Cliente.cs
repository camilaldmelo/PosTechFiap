﻿namespace Domain.Entities
{
    public class Cliente
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public string? Email { get; set; }
        public required DateTime Data { get; set; }
    }
}
