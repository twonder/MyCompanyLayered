using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyCompany.Domain.Models;

namespace MyCompany.Domain.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);

        IEnumerable<Order> FindBy(Expression<Func<Order, bool>> predicate);

        void Delete(Order entity);
    }
}
