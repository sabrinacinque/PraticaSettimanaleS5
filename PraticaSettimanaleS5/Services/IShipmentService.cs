using PraticaSettimanaleS5.Models;
using System.Collections.Generic;

namespace PraticaSettimanaleS5.Services
{
    public interface IShipmentService
    {
        List<StatoSpedizione> GetShipmentUpdates(string idNumber, string trackingNumber);
    }
}
