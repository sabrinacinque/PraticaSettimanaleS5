namespace PraticaSettimanaleS5.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public bool ÈAzienda { get; set; }
        public string CodiceFiscale { get; set; }
        public string PartitaIVA { get; set; }
        public string Indirizzo { get; set; }
        public string Città { get; set; }
        public string NumeroTelefono { get; set; }
        public string Email { get; set; }
    }
}
