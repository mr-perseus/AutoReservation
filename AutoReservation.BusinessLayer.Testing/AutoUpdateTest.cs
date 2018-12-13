using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class AutoUpdateTests
        : TestBase
    {
        private AutoManager _target;
        private AutoManager Target => _target ?? (_target = new AutoManager());

        [Fact]
        public void UpdateAutoTest()
        {
            Auto auto = Target.List[0];
            auto.Tagestarif = 500;
            Target.Update(auto);

            Auto autoActual = Target.GetById(auto.Id);

            Assert.Equal(auto.Tagestarif, autoActual.Tagestarif);
        }
    }
}