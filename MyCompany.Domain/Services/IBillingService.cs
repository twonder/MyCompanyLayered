using MyCompany.Domain.Models;

namespace MyCompany.Domain.Services
{
    public interface IBillingService
    {
        void CreateACustomerIfNotYetCreated(Customer customer);
        double GetBalanceForCustomer(string customerId);
        void DebitCustomerBalance(double amountToDebit);
    }
}
