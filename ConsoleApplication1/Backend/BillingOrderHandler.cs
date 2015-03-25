using MyCompany.Domain.Messages;
using NServiceBus;

namespace MyCompany.Domain.Backend
{
    public class BillingOrderHandler : IHandleMessages<OrderAccepted>
    {
        public void Handle(OrderAccepted orderDetails)
        {
            // create a billing customer if not already there
        }
    }
}
