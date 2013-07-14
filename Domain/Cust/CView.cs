

using Data;

namespace Domain.Cust
{
    public class CView
    {
        public long Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }
        public string Button { get; set; }
        public static CView BlankView()
        {
            return new CView() { Number = 0, Name = "", Address = "", Mobile = "", Message = "-" };
        }

        public const int NumberLength = 10, NameLength = 20, AddressLength = 60, MobileLength = 12, IdLength = 10;
        public void CopyToCustomer(Customer c)
        {
            c.number = Number;
            c.name = Name;
            c.address = Address;
            c.mobile = Mobile;
        }
        public void CopyFromCustomer(Customer c)
        {
            Number = c.number;
            Name = c.name;
            Address = c.address;
            Mobile = c.mobile;
        }
    }
}