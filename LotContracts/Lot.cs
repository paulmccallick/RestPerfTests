using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotContracts
{
    public class Lot
    {
        public int LotId { get; set; }
        public int PortId { get; set; }
        public int SecurityId { get; set; }
        public decimal ShareCount { get; set; }
        public decimal CostBasis { get; set; }
        public DateTime BuyDate { get; set; }
    }
}
