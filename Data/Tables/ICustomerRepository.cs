namespace Data.Tables
{
    public interface ICustomerRepository
    {
        Customer LastCustomer { get; }
        bool CheckCustomerNumber(long number);
        void DeleteCustomer();
        void AddCustomer(Customer c);
    }
}