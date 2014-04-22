using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using Enyim.Caching.Memcached;
using ProtoBuf;

namespace LotService
{
    public class CouchBaseHelper<T>
    {
        public T GetFromCache(string cacheKey)
        {
            var client = CouchbaseManager.Instance;
            var bytes = client.Get<byte[]>(cacheKey);

            if (bytes == null)
                return default(T);
            using (var stream = new MemoryStream(bytes))
            {
                return Serializer.Deserialize<T>(stream);
            }
        }

        public void StoreInCache(StoreMode storeMode, string key, T entity)
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
