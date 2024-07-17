namespace PraticaSettimanaleS5.Models
{
    public class ReportViewModel
    {
        public List<Spedizione> TodayShipments { get; set; } = new List<Spedizione>();
        public int PendingShipmentsCount { get; set; }
        public Dictionary<string, int> ShipmentsByCity { get; set; } = new Dictionary<string, int>();
    }
}
