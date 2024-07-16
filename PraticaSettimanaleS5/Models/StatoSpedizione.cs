namespace PraticaSettimanaleS5.Models
{
    public class StatoSpedizione
    {
        public int StatoId { get; set; }
        public int SpedizioneId { get; set; }
        public string Stato { get; set; }
        public string Luogo { get; set; }
        public string Descrizione { get; set; }
        public DateTime DataAggiornamento { get; set; }

        public Spedizione Spedizione { get; set; }
    }
}
