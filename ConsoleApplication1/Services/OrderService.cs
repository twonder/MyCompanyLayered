using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyCompany.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBillingService _billingService;
        private readonly IEmailService _emailService;

        public OrderService(IOrderRepository orderRepository, 
            IBillingService billingService, 
            IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _billingService = billingService;
            _emailService = emailService;
        }

        public void CreateOrder(Order order)
        {
            // save it
            _orderRepository.Save(order);

            // make sure billing knows about this, we know about them
            var customer = new Customer {FirstName = order.CustomerFirstName, 
                LastName = order.CustomerLastName};
            _billingService.CreateACustomerIfNotYetCreated(customer);

            // send communication
            _emailService.SendEmail("info@mycompany.com", order.CustomerEmail, "Thanks");
        }
    }
}

namespace MyCompany.Domain.Services
{
    public interface IOrderService
    {
        void CreateOrder(Order order);
    }

    public interface IEmailService
    {
        void SendEmail(string to, string from, string body);
    }

    public interface IOrderRepository
    {
        void Save(Order order);
    }

    public class Order
    {
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public decimal Amount { get; set; }
    }

    public interface IBillingService
    {
        void CreateACustomerIfNotYetCreated(Customer customer);
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
