using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotContracts;
using LotService;
using NUnit.Framework;

namespace LotServiceTests
{
    [TestFixture]
    public class CouchBaseLotSpeedTests
    {
        [TestFixtureSetUp]
        public void BeforeAll()
        {
            LotRepository.UseCouch = true;
        }

        [Test]
        public async void Get1000Lots100TimesViaHttpWithCouchbase()
        {
            await TestHelpers.GetPortfolioLots(100, true);
        }

        [Test]
        public void Get1000Lots100TimesViaCouchbase()
        {
            LotRepository repo;
            List<Lot> lots;
            for (int i = 0; i < 100; i++)
            {
                var start = DateTime.Now;
                repo = new LotRepository();
                lots = repo.GetLots().ToList();
                Console.WriteLine(String.Format("time to get lots: {0}", (DateTime.Now - start).TotalMilliseconds));
            }
            
        }

        [Test]
        public async void Get1Lot100TimesViaHttpWithCouchbase()
        {
            await TestHelpers.Get1Lot(100,true);
        }

        [Test]
        public void Get1Lot100TimesViaCouchbase()
        {
            LotRepository repo;
            Lot lot;
            for (int i = 0; i < 100; i++)
            {
                var start = DateTime.Now;
                repo = new LotRepository();
                lot = repo.GetLot();
                Console.WriteLine(String.Format("time to get lots: {0}", (DateTime.Now - start).TotalMilliseconds));
            }

        }


    }
}
