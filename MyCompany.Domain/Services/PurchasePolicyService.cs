using System;
using System.Collections.Generic;
using System.Linq;
using MyCompany.Domain.Models;
using MyCompany.Domain.Repositories;
using MyCompany.Messages;
using NServiceBus;
using Order = MyCompany.Domain.Models.Order;

namespace MyCompany.Domain.Services
{
    public class PurchasePolicyService
    {
        private IOrderRepository _orderRepository;
        private IBillingService _billingService;
        private IBus _bus;
        private double _discountTotalThreshold = 100;

        public PurchasePolicyService(IOrderRepository orderRepository, IBillingService billingService, IBus bus)
        {
            _orderRepository = orderRepository;
            _billingService = billingService;
            _bus = bus;
        }

        /*
         * Assuming we have the customer object at this point, otherwise would need to look that up here
         * with a customer repository/service
         */
        public void CreateOrder(Order order, Customer customer)
        {
            // 2 weeks for preferred, 1 for non-preferred
            var daysToConsider = customer.Preferred ? 14 : 7;
            
            // would probably have to use SqlFunctions.DateDiff
            var ordersToConsider = _orderRepository.FindBy(o => o.CustomerId == customer.Id 
                && o.DateOccured > DateTime.Now.AddDays(daysToConsider * -1)).ToList();

            // calculate the discount and apply it to order amount
            var discount = CalculateDiscount(customer.Preferred, ordersToConsider);
            var totalAfterDiscount = order.Amount - (order.Amount * discount);

            // make sure they have enough moola
            var customerBalance = _billingService.GetBalanceForCustomer(customer.Id);
            if (totalAfterDiscount > customerBalance)
            {
                // return an error or something
            }
            
            // publish the event
            _bus.Publish<OrderCompleted>(o =>
            {
                o.DateOccurred = DateTime.Now;
                o.OrderId = order.OrderId;
                o.CustomerId = order.CustomerId;
                o.Amount = totalAfterDiscount;
            });
        }

        private double CalculateDiscount(bool customerPreferred, List<Order> orders)
        {
            var total = orders.Sum(o => o.Amount);

            if (customerPreferred)
            {
                return total > _discountTotalThreshold ? 0.20 : 0.10;
            }
            else
            {
                return total > _discountTotalThreshold ? 0.10 : 0;
            }
        }
    }
}
