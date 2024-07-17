namespace PraticaSettimanaleS5.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool ÈAzienda { get; set; }
        public string? CodiceFiscale { get; set; }
        public string? PartitaIVA { get; set; }
        public string Indirizzo { get; set; } = string.Empty;
        public string Città { get; set; } = string.Empty;
        public string NumeroTelefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
