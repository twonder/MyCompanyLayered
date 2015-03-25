using MyCompany.Domain.Messages;
using NServiceBus;

namespace MyCompany.Domain.Backend
{
    public class EmailHandler : IHandleMessages<OrderAccepted>
    {
        public void Handle(OrderAccepted message)
        {
            // email the happy customer
        }
    }
}
