using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Messages
{
    public interface OrderCompleted
    {
        DateTime DateOccurred { get; set; }
        string OrderId { get; set; }
        string CustomerId { get; set; }
        string ProductId { get; set; }
        double Amount { get; set; }
    }
}
