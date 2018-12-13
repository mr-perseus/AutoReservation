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
            Reservation reservation = Target.GetById(1);
            reservation.Von = DateTime.Today;
            reservation.Bis = DateTime.Today.AddDays(1);
            Target.Update(reservation);
        }

        private ReservationManager _target;
        private ReservationManager Target => _target ?? (_target = new ReservationManager());

        [Fact]
        public void ScenarioNotOkay01Test()
        {
            throw new NotImplementedException("Test not implemented.");
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
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void ScenarioOkay02Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void ScenarioOkay03Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void ScenarioOkay04Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }
    }
}