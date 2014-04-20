using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async void Get1000Lots100TimesByProtobuf()
        {
            LotRepository.UseCouch = true;
            await TestHelpers.GetPortfolioLots(100, true);
        }
    }
}
