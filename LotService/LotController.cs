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

        [Route("portfolio/{portfolioId}/lots")]
        public IEnumerable<Lot> GetLotsById(int portfolioId)
        {
            var lots = Enumerable.Range(1, 1000).Select(i => new Lot {LotId = i,PortId = portfolioId});
            return lots;
        }

        [Route("lot/{lotId}")]
        public Lot GetLotById(int lotId)
        {
            return new Lot {LotId = lotId};
        }
    }
}
