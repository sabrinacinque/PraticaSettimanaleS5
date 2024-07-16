namespace PraticaSettimanaleS5.Models
{
    public class Spedizione
    {
        public int SpedizioneId { get; set; }
        public int ClienteId { get; set; }
        public string NumeroTracking { get; set; }
        public DateTime DataSpedizione { get; set; }
        public decimal Peso { get; set; }
        public string CittàDestinazione { get; set; }
        public string IndirizzoDestinazione { get; set; }
        public string NomeDestinatario { get; set; }
        public decimal CostoSpedizione { get; set; }
        public DateTime DataConsegnaPrevista { get; set; }

        public Cliente Cliente { get; set; }
    }
}
