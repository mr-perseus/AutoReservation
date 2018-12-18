using System;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationDateRangeTest
        : TestBase
    {
        private ReservationManager _target;
        private ReservationManager Target => _target ?? (_target = new ReservationManager());

        [Fact]
        public void BisEarlierThanVonThrowsExceptionTest()
        {
            Reservation reservation = Target.GetById(1);
            reservation.Bis = DateTime.Today;
            reservation.Von = DateTime.Today.AddDays(1);
            Assert.Throws<InvalidDateRangeException>(() => Target.Update(reservation));
        }

        [Fact]
        public void BisNotTwentyFourHoursLaterThrowsExceptionTest()
        {
            Reservation reservation = Target.GetById(1);
            reservation.Von = new DateTime(2012, 12, 21, 11, 11, 11);
            reservation.Bis = new DateTime(2012, 12, 22, 11, 10, 10);
            Assert.Throws<InvalidDateRangeException>(() => Target.Update(reservation));
        }

        [Fact]
        public void BisSameAsVonThrowsExceptionTest()
        {
            DateTime dateTime = new DateTime(2012, 12, 21, 10, 10, 10);
            Reservation reservation = Target.GetById(1);
            reservation.Von = dateTime;
            reservation.Bis = dateTime;
            Assert.Throws<InvalidDateRangeException>(() => Target.Update(reservation));
        }

        [Fact]
        public void ScenarioOkay01Test()
        {
            Reservation reservation = Target.GetById(1);
            reservation.Von = DateTime.Today;
            reservation.Bis = DateTime.Today.AddDays(1);
            Target.Update(reservation);

            Reservation actualReservation = Target.GetById(1);
            Assert.Equal(DateTime.Today, actualReservation.Von);
            Assert.Equal(DateTime.Today.AddDays(1), actualReservation.Bis);
        }

        [Fact]
        public void ScenarioOkay02Test()
        {
            Reservation reservation = Target.GetById(1);
            reservation.Von = new DateTime(2012, 12, 21, 10, 10, 10);
            reservation.Bis = new DateTime(2012, 12, 22, 10, 10, 10);
            Target.Update(reservation);

            Reservation actualReservation = Target.GetById(1);
            Assert.Equal(new DateTime(2012, 12, 21, 10, 10, 10), actualReservation.Von);
            Assert.Equal(new DateTime(2012, 12, 22, 10, 10, 10), actualReservation.Bis);
        }
    }
}