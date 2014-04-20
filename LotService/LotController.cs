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
        private readonly LotRepository _repository;

        public LotController()
        {
            _repository = new LotRepository();
        }
        [Route("portfolio/{portfolioId}/lots")]
        public IEnumerable<Lot> GetLotsById(int portfolioId)
        {
            var lots = _repository.GetLots();
            return lots;
        }

        [Route("lot/{lotId}")]
        public Lot GetLotById(int lotId)
        {
            return new Lot {LotId = lotId};
        }
    }
}
