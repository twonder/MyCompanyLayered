using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Domain.Services.Events;
using NServiceBus;

namespace MyCompany.Domain.Services.Backend
{
    public class EmailHandler : IHandleMessages<OrderAccepted>
    {
        public void Handle(OrderAccepted message)
        {
            // email the happy customer
        }
    }
}
