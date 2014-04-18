using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LotContracts;

namespace LotService
{
    public class LotController: ApiController
    {
        //Lot/{id}
        public IEnumerable<Lot> GetLotsById()
        {
          
            var lots = Enumerable.Range(1, 1000).Select(i => new Lot {LotId = i});
           
            return lots;
        }
    }
}
