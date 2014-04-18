using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace LotContracts
{
    [ProtoContract]
    public class Lot
    {
        [ProtoMember(1)]
        public int LotId { get; set; }
        [ProtoMember(2)]
        public int PortId { get; set; }
        [ProtoMember(3)]
        public int SecurityId { get; set; }
        [ProtoMember(4)]
        public decimal ShareCount { get; set; }
        [ProtoMember(5)]
        public decimal CostBasis { get; set; }
        [ProtoMember(6)]
        public DateTime BuyDate { get; set; }
    }
}
