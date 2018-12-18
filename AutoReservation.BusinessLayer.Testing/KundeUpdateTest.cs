using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class KundeUpdateTest
        : TestBase
    {
        private KundeManager _target;
        private KundeManager Target => _target ?? (_target = new KundeManager());

        [Fact]
        public void UpdateKundeTest()
        {
            Kunde kunde = Target.List[0];
            kunde.Nachname = "Smith";
            Target.Update(kunde);

            Kunde actualKunde = Target.GetById(kunde.Id);

            Assert.Equal(kunde.Nachname, actualKunde.Nachname);
        }
    }
}