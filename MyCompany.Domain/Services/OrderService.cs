using MyCompany.Domain.Messages;
using MyCompany.Domain.Repositories;
using NServiceBus;
using Order = MyCompany.Domain.Models.Order;

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
}
