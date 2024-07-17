namespace PraticaSettimanaleS5.Models
{
    public class StatoSpedizione
    {
        public int StatoSpedizioneId { get; set; }
        public string NumeroTracking { get; set; } = string.Empty;
        public DateTime DataAggiornamento { get; set; }
        public string Stato { get; set; } = string.Empty;
        public string Luogo { get; set; } = string.Empty;
        public string? Descrizione { get; set; }
        public Spedizione? Spedizione { get; set; }
    }
}
