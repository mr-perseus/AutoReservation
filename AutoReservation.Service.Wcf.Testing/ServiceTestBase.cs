using System;
using System.ServiceModel;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.Service.Wcf.Testing
{
    public abstract class ServiceTestBase
        : TestBase
    {
        protected abstract IAutoReservationService Target { get; }

        #region Read all entities

        [Fact]
        public void GetAutosTest()
        {
            var autoList = Target.AutoList();

            Assert.NotNull(autoList);
            Assert.True(autoList.Count > 0);

        }

        [Fact]
        public void GetKundenTest()
        {
            var kundeList = Target.KundeList();

            Assert.NotNull(kundeList);
            Assert.True(kundeList.Count > 0);

        }

        [Fact]
        public void GetReservationenTest()
        {
            var reservationList = Target.ReservationList();

            Assert.NotNull(reservationList);
            Assert.True(reservationList.Count > 0);
        }

        #endregion

        #region Get by existing ID

        [Fact]
        public void GetAutoByIdTest()
        {
            AutoDto auto = Target.GetAutoById(1);

            Assert.Equal(1, auto.Id);
        }

        [Fact]
        public void GetKundeByIdTest()
        {
            KundeDto kunde = Target.GetKundeById(1);

            Assert.Equal(1,kunde.Id);
        }

        [Fact]
        public void GetReservationByNrTest()
        {
            ReservationDto reservation = Target.GetReservationById(1);

            Assert.Equal(1, reservation.ReservationsNr);
        }

        #endregion

        #region Get by not existing ID

        [Fact]
        public void GetAutoByIdWithIllegalIdTest()
        {
            AutoDto auto = Target.GetAutoById(-1);

            Assert.Null(auto);
        }

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
            KundeDto kunde = Target.GetKundeById(-1);
            
            Assert.Null(kunde);
        }

        [Fact]
        public void GetReservationByNrWithIllegalIdTest()
        {
            ReservationDto reservaton = Target.GetReservationById(-1);

            Assert.Null(reservaton);
        }

        #endregion

        #region Insert

        [Fact]
        public void InsertAutoTest()
        {
            AutoDto newauto = new AutoDto {Basistarif = 50, Marke = "Chrysler", Tagestarif = 60};
                
            Target.InsertAuto(newauto);
            AutoDto autotocompare = Target.GetAutoById(5);
            Assert.Equal(newauto.Marke, autotocompare.Marke);
            Assert.Equal(newauto.Basistarif, autotocompare.Basistarif);
            Assert.Equal(newauto.Tagestarif, autotocompare.Tagestarif);
        }

        [Fact]
        public void InsertKundeTest()
        {
            KundeDto newkunde = new KundeDto
            {
                Nachname = "Muster",
                Vorname =  "Max",
                Geburtsdatum = new DateTime(1985, 10, 25)
            };

            Target.InsertKunde(newkunde);
            KundeDto kundetocompare = Target.GetKundeById(5);
            Assert.Equal(newkunde.Nachname, kundetocompare.Nachname);
            Assert.Equal(newkunde.Geburtsdatum, kundetocompare.Geburtsdatum);
            Assert.Equal(newkunde.Vorname, kundetocompare.Vorname);
        }

        [Fact]
        public void InsertReservationTest()
        {
            ReservationDto newreservation = new ReservationDto
            {
                Auto = Target.GetAutoById(1),
                Kunde = Target.GetKundeById(1),
                Von = new DateTime(2018, 12, 18),
                Bis = new DateTime(2019, 01, 01)
            };

            Target.InsertReservation(newreservation);
            ReservationDto reservationtocompare = Target.GetReservationById(5);
            Assert.Equal(newreservation.Auto.Id, reservationtocompare.Auto.Id);
            Assert.Equal(newreservation.Kunde.Id, reservationtocompare.Kunde.Id);
            Assert.Equal(newreservation.Von, newreservation.Von);
            Assert.Equal(newreservation.Bis, reservationtocompare.Bis);


        }

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
            AutoDto autotodelete = Target.GetAutoById(4);
            Target.DeleteAuto(autotodelete);

            AutoDto autodeleted = Target.GetAutoById(4);
            Assert.Null(autodeleted);
        }

        [Fact]
        public void DeleteKundeTest()
        {
            KundeDto kundetodelete = Target.GetKundeById(4);
            Target.DeleteKunde(kundetodelete);

            KundeDto kundedeleted = Target.GetKundeById(4);
            Assert.Null(kundedeleted);
        }

        [Fact]
        public void DeleteReservationTest()
        {
            ReservationDto reservationtodelete = Target.GetReservationById(4);
            Target.DeleteReservation(reservationtodelete);

            ReservationDto reservationdeleted = Target.GetReservationById(4);
            Assert.Null(reservationdeleted);
        }

        #endregion

        #region Update

        [Fact]
        public void UpdateAutoTest()
        {
            AutoDto autotoupdate = Target.GetAutoById(4);
            autotoupdate.Marke = "Porsche";
            Target.UpdateAuto(autotoupdate);

            AutoDto autoupdated = Target.GetAutoById(4);

            Assert.Equal(autotoupdate.Marke, autoupdated.Marke);

        }

        [Fact]
        public void UpdateKundeTest()
        {
            KundeDto kundetoupdate = Target.GetKundeById(4);
            kundetoupdate.Nachname = "Jobs";
            Target.UpdateKunde(kundetoupdate);

            KundeDto kundeupdated = Target.GetKundeById(4);
            Assert.Equal(kundetoupdate.Nachname, kundeupdated.Nachname);
            
        }

        [Fact]
        public void UpdateReservationTest()
        {
            ReservationDto reservationtoupdate = Target.GetReservationById(4);
            reservationtoupdate.Bis = new DateTime(2019, 12, 10);
            Target.UpdateReservation(reservationtoupdate);

            ReservationDto reservationupdated = Target.GetReservationById(4);

            Assert.Equal(reservationtoupdate.Bis, reservationupdated.Bis);

        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            AutoDto autotoupdate1 = Target.GetAutoById(4);
            autotoupdate1.Basistarif = 55;

            AutoDto autotoupdate2 = Target.GetAutoById(4);
            autotoupdate2.Basistarif = 44;

            Target.UpdateAuto(autotoupdate1);

            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() => Target.UpdateAuto(autotoupdate2));

        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            KundeDto kundetoupdate1 = Target.GetKundeById(4);
            kundetoupdate1.Nachname = "Gates";

            KundeDto kundetoupdate2 = Target.GetKundeById(4);
            kundetoupdate2.Nachname = "Jobs";

            Target.UpdateKunde(kundetoupdate1);

            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() => Target.UpdateKunde(kundetoupdate2));

        }

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            ReservationDto reservationtoupdate1 = Target.GetReservationById(4);
            reservationtoupdate1.Bis = new DateTime(2019, 06, 06);

            ReservationDto reservationtoupdate2 = Target.GetReservationById(4);
            reservationtoupdate2.Bis = new DateTime(2019, 07, 07);

            Target.UpdateReservation(reservationtoupdate1);

            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() => Target.UpdateReservation(reservationtoupdate2));
        }

        #endregion

        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            ReservationDto newreservation = new ReservationDto
            {
                Auto = Target.GetAutoById(1),
                Kunde = Target.GetKundeById(1),
                Von = new DateTime(2018, 12, 18),
                Bis = new DateTime(2018, 12, 17)
            };

            //Assert.Throws<FaultException<>>(() => Target.InsertReservation(newreservation));


        }

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Check Availability

        [Fact]
        public void CheckAvailabilityIsTrueTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void CheckAvailabilityIsFalseTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion
    }
}
