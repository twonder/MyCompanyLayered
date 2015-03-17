using MyCompany.Domain.Messages;
using NServiceBus;

namespace MyCompany.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBus _bus;

        public OrderService(IOrderRepository orderRepository, IBus bus)
        {
            _orderRepository = orderRepository;
            _bus = bus;
        }

        public void CreateOrder(Order order)
        {
            // save it
            _orderRepository.Save(order);

            // tell others that care
            _bus.Publish<OrderAccepted>(o =>
            {
                o.FirstName = order.CustomerFirstName;
                o.LastName = order.CustomerLastName;
                o.Email = order.CustomerEmail;
            });
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
