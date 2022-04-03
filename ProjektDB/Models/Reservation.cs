using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDB.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Length { get; set; }
        public string AddInfo { get; set; }
        public string State { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
    }
}
