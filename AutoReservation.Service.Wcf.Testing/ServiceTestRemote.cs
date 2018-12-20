using System;
using System.ServiceModel;
using AutoReservation.Common.Interfaces;
using Xunit;

namespace AutoReservation.Service.Wcf.Testing
{
    public class ServiceTestRemote
        : ServiceTestBase
            , IClassFixture<ServiceTestRemoteFixture>
    {
        private readonly ServiceTestRemoteFixture _serviceTestRemoteFixture;

        private IAutoReservationService _target;

        public ServiceTestRemote(ServiceTestRemoteFixture serviceTestRemoteFixture)
        {
            _serviceTestRemoteFixture = serviceTestRemoteFixture;
        }

        protected override IAutoReservationService Target
        {
            get
            {
                if (_target == null)
                {
                    var channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
                    _target = channelFactory.CreateChannel();
                }

                return _target;
            }
        }
    }

    public class ServiceTestRemoteFixture
        : IDisposable
    {
        public ServiceTestRemoteFixture()
        {
            ServiceHost = new ServiceHost(typeof(AutoReservationService));
            ServiceHost.Open();
        }

        public ServiceHost ServiceHost { get; }

        public void Dispose()
        {
            if (ServiceHost.State != CommunicationState.Closed) ServiceHost.Close();
        }
    }
}