using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationAvailabilityTest
        : TestBase
    {
        public ReservationAvailabilityTest()
        {
            // Prepare reservation

        }

        private ReservationManager _target;
        private ReservationManager Target => _target ?? (_target = new ReservationManager());

        [Fact]
        public void ScenarioNotOkay01Test()
        {

        }

        [Fact]
        public void ScenarioNotOkay02Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void ScenarioNotOkay03Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void ScenarioNotOkay04Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void ScenarioNotOkay05Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void ScenarioOkay01Test()
        {
            Reservation reservation = Target.GetById(1);
            reservation.AutoId = 2;
            reservation.Von = DateTime.Today;
            reservation.Bis = DateTime.Today.AddDays(1);
            Target.Update(reservation);

            Reservation actualReservation = Target.GetById(1);
            Assert.Equal(2, actualReservation.AutoId);
            Assert.Equal(DateTime.Today, actualReservation.Von);
            Assert.Equal(DateTime.Today.AddDays(1), actualReservation.Bis);
        }

        [Fact]
        public void ScenarioOkay02Test()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 3,
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(1),
                KundeId = 4
            };
            Target.Add(reservation);

            Reservation actualReservation = Target.GetById(5);
            Assert.Equal(3, actualReservation.AutoId);
            Assert.Equal(DateTime.Today, actualReservation.Von);
            Assert.Equal(DateTime.Today.AddDays(1), actualReservation.Bis);
            Target.Remove(reservation);
        }

        [Fact]
        public void ScenarioOkay03Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void ScenarioOkay04Test()
        {

        }
    }
}