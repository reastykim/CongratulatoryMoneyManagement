using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongratulatoryMoneyManagement.Models
{
    public class StatementItem
    {
        public Type ItemType { get; set; }
        public string ItemTypeDisplay { get; set; }
        public int Id { get; set; }
        public string Details { get; set; }
        public double Sum { get; set; }
        public DateTime Created { get; set; }
    }
}
