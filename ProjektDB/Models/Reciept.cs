using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDB.Models
{
    public class Reciept
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string State { get; set; }
        public int UserId { get; set; }
        public int StockId { get; set; }
    }
}
