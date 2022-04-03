using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDB.Models
{
    public class Requirement
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int ReservationId { get; set; }
        public int StockId { get; set; }
    }
}
