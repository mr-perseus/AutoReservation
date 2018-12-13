using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationUpdateTest
        : TestBase
    {
        private ReservationManager _target;
        private ReservationManager Target => _target ?? (_target = new ReservationManager());

        [Fact]
        public void UpdateReservationTest()
        {
            Reservation reservation = Target.List[0];
            reservation.Bis = new DateTime(2022, 1, 1);
            Target.Update(reservation);

            Reservation reservationActual = Target.GetById(reservation.ReservationsNr);

            Assert.Equal(reservation.Bis, reservationActual.Bis);
        }
    }
}