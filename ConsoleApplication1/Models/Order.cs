using System;

namespace MyCompany.Domain.Models
{
    public class Order
    {
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerId { get; set; }
        public double Amount { get; set; }
        public DateTime DateOccured { get; set; }
        public string OrderId { get; set; }
    }
}
