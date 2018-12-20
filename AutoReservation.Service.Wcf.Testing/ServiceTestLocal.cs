using AutoReservation.Common.Interfaces;

namespace AutoReservation.Service.Wcf.Testing
{
    public class ServiceTestLocal
        : ServiceTestBase
    {
        private IAutoReservationService _target;
        protected override IAutoReservationService Target => _target ?? (_target = new AutoReservationService());
    }
}