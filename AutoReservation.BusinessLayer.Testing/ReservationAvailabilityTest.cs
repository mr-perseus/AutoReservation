using System;
using AutoReservation.BusinessLayer.Exceptions;
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
            Reservation reservation = new Reservation
            {
                AutoId = 3,
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(1),
                KundeId = 3
            };
            Reservation reservationSameTimeSameCar = new Reservation
            {
                AutoId = 3,
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(1),
                KundeId = 2
            };
            Target.Add(reservation);
            Assert.Throws<AutoUnavailableException>(() => Target.Add(reservationSameTimeSameCar));
            Target.Remove(reservation);
        }

        [Fact]
        public void ScenarioNotOkay02Test()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 3,
                Von = DateTime.Today.AddDays(5),
                Bis = DateTime.Today.AddDays(10),
                KundeId = 3
            };
            Reservation reservationLeftTimeOverlapping = new Reservation
            {
                AutoId = 3,
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(6),
                KundeId = 2
            };
            Target.Add(reservation);
            Assert.Throws<AutoUnavailableException>(() => Target.Add(reservationLeftTimeOverlapping));
            Target.Remove(reservation);
        }

        [Fact]
        public void ScenarioNotOkay03Test()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 3,
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(6),
                KundeId = 3
            };
            Reservation reservationRightTimeOverlapping = new Reservation
            {
                AutoId = 3,
                Von = DateTime.Today.AddDays(5),
                Bis = DateTime.Today.AddDays(10),
                KundeId = 2
            };
            Target.Add(reservation);
            Assert.Throws<AutoUnavailableException>(() => Target.Add(reservationRightTimeOverlapping));
            Target.Remove(reservation);
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
                KundeId = 3
            };
            Target.Add(reservation);

            Reservation actualReservation = Target.GetLastReservation();
            Assert.Equal(3, actualReservation.AutoId);
            Assert.Equal(DateTime.Today, actualReservation.Von);
            Assert.Equal(DateTime.Today.AddDays(1), actualReservation.Bis);
            Target.Remove(reservation);
        }

        [Fact]
        public void ScenarioOkay03Test()
        {
            Reservation reservation = Target.GetById(1);
            reservation.AutoId = 2;
            reservation.Von = new DateTime(2012, 12, 21, 10, 10, 10);
            reservation.Bis = new DateTime(2012, 12, 22, 10, 10, 10);
            reservation.KundeId = 3;
            Target.Update(reservation);

            Reservation actualReservation = Target.GetById(1);
            Assert.Equal(2, actualReservation.AutoId);
            Assert.Equal(new DateTime(2012, 12, 21, 10, 10, 10), actualReservation.Von);
            Assert.Equal(new DateTime(2012, 12, 22, 10, 10, 10), actualReservation.Bis);
            Assert.Equal(3, actualReservation.KundeId);
        }

        [Fact]
        public void ScenarioOkay04Test()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 3,
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(6),
                KundeId = 3
            };
            Reservation reservationLater = new Reservation
            {
                AutoId = 3,
                Von = DateTime.Today.AddDays(6),
                Bis = DateTime.Today.AddDays(10),
                KundeId = 2
            };
            Target.Add(reservation);

            Reservation actualReservation = Target.GetLastReservation();
            Assert.Equal(3, actualReservation.AutoId);
            Assert.Equal(DateTime.Today, actualReservation.Von);
            Assert.Equal(DateTime.Today.AddDays(6), actualReservation.Bis);
            Assert.Equal(3, actualReservation.KundeId);


            Target.Add(reservationLater);

            Reservation actualReservationLater = Target.GetLastReservation();
            Assert.Equal(3, actualReservationLater.AutoId);
            Assert.Equal(DateTime.Today.AddDays(6), actualReservationLater.Von);
            Assert.Equal(DateTime.Today.AddDays(10), actualReservationLater.Bis);
            Assert.Equal(2, actualReservationLater.KundeId);

            Target.Remove(reservation);
            Target.Remove(reservationLater);
        }
    }
}