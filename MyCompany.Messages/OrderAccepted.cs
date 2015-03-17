using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Domain.Messages
{
   public interface OrderAccepted
   {
       string FirstName { get; set; }
       string LastName { get; set; }
       string Email { get; set; }
   }
}
