namespace PraticaSettimanaleS5.Models
{
    public class Spedizione
    {
        public int SpedizioneId { get; set; }
        public int ClienteId { get; set; }
        public string NumeroTracking { get; set; } = string.Empty;
        public DateTime DataSpedizione { get; set; }
        public decimal Peso { get; set; }
        public string CittàDestinazione { get; set; } = string.Empty;
        public string IndirizzoDestinazione { get; set; } = string.Empty;
        public string NomeDestinatario { get; set; } = string.Empty;
        public decimal CostoSpedizione { get; set; }
        public DateTime DataConsegnaPrevista { get; set; }

        public Cliente? Cliente { get; set; }
        public string? UltimoStato { get; set; } // Nuova proprietà
    }
}
