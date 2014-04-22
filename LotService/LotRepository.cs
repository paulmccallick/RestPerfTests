using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Configuration;
using Enyim.Caching.Memcached;
using LotContracts;
using ProtoBuf;

namespace LotService
{
    public class LotRepository

    {
        private const string lots_key = "port_id_1_lots";
        private const string lot_key = "lot_id_i_lot";
        private static bool _lotsStashed = false;
        private readonly CouchBaseHelper<Lot> _lotHelper;
        private readonly CouchBaseHelper<IEnumerable<Lot>> _lotCollectionHelper; 

        public LotRepository()
        {
            if (UseCouch)
            {
                _lotHelper = new CouchBaseHelper<Lot>();
                _lotCollectionHelper = new CouchBaseHelper<IEnumerable<Lot>>();
                PrepCache();

            }
        }

        public static bool UseCouch { get; set; }

        public  void PrepCache()
        {
            if (!_lotsStashed)
            {
                var lots = Enumerable.Range(1, 1000).Select(i => new Lot {LotId = i, PortId = 1});
                _lotCollectionHelper.StoreInCache(StoreMode.Set, lots_key, lots);
                var lot = lots.First();
                _lotHelper.StoreInCache(StoreMode.Set,lot_key,lot);
                _lotsStashed = true;
            }
        }

        public IEnumerable<Lot> GetLots()
        {   
            if(UseCouch)
                return _lotCollectionHelper.GetFromCache(lots_key);
            return Enumerable.Range(1, 1000).Select(i => new Lot { LotId = i, PortId = 1 });
        }

        public Lot GetLot()
        {
            if (UseCouch)
                return _lotHelper.GetFromCache(lot_key);
            return new Lot {LotId = 1, PortId = 1};
        }




    }
}
