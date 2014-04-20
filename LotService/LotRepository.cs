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
        private const string lot_key = "port_id_1_lots";
        private static bool _lotsStashed = false;

        public LotRepository()
        {
            if (UseCouch)
            {
                PrepCache();
            }
        }

        public static bool UseCouch { get; set; }

        public  void PrepCache()
        {
            if (!_lotsStashed)
            {
                var lots = Enumerable.Range(1, 1000).Select(i => new Lot {LotId = i, PortId = 1});
                StoreInCache(StoreMode.Set, lot_key, lots);
                _lotsStashed = true;
            }
        }

        public IEnumerable<Lot> GetLots()
        {   
            if(UseCouch)
                return GetFromCache(lot_key);
            else
                return Enumerable.Range(1, 1000).Select(i => new Lot { LotId = i, PortId = 1 });
        }

        private IEnumerable<Lot> GetFromCache(string cacheKey)
        {
            var client = CouchbaseManager.Instance;
            var bytes = client.Get<byte[]>(cacheKey);

            if (bytes == null)
                return null;
            using (var stream = new MemoryStream(bytes))
            {
                return Serializer.Deserialize<IEnumerable<Lot>>(stream);
            }
        }

        private void StoreInCache(StoreMode storeMode, string key, IEnumerable<Lot> entity)
        {
            if (entity == null)
                return;
            using (var stream = new MemoryStream())
            {
                var client = CouchbaseManager.Instance;
                Serializer.Serialize(stream, entity);
                var bytes = stream.ToArray();
                if (client.Store(storeMode, key, bytes))
                    return;
                // If the store failed, try again with ExecuteStore to get info on the failure
                var result = client.ExecuteStore(storeMode, key, bytes);
                const string messageFormat = "Failed to store key {0}:\n{1}";
                var message = string.Format(messageFormat, key, result.Message);
                if (result.Exception == null)
                    throw new ApplicationException(message);
                else
                    throw new ApplicationException(message, result.Exception);
            }
        }

    }
}
