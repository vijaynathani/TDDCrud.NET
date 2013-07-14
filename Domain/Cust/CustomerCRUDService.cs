using System.Text.RegularExpressions;
using Data;
using Data.Tables;

namespace Domain.Cust
{
    public class CustomerCRUDService
    {
        public const string InvalidOperation = "Invalid Operation",
                              InformationRetrieved = "Information Retrieved",
                              CustomerDeleted = "Customer Deleted",
                              InformationUpdated = "Information Updated",
                              CustomerAdded = "Customer Added",
                              ViewFirst = "Please View the information first",
                              CustomerNumberAbsent = "Customer number not Found",
                              CustomerNumberPresent = "Customer Number already present",
                              CustomerNumberInvalid = "Customer Number Invalid",
                              NameInvalid = "Name Invalid",
                              AddressInvalid = "Address Invalid",
                              MobileInvalid = "Mobile Number Invalid",
                              AddOp = "Add",
                              ChgOp = "Chg",
                              ViewOp = "View",
                              DelOp = "Del";

        private readonly CView _c;
        private readonly ICustomerRepository _dbOps;
        private readonly long _oldNumber;

        public CustomerCRUDService(CView c, long oldNumber, ICustomerRepository dbOps)
        {
            _dbOps = dbOps;
            _c = c;
            _oldNumber = oldNumber;
        }

        public void Process()
        {
            switch (_c.Button)
            {
                case AddOp:
                    AddOperation();
                    break;
                case ChgOp:
                    ChangeOperation();
                    break;
                case ViewOp:
                    ViewOperation();
                    break;
                case DelOp:
                    DeleteOperation();
                    break;
                default:
                    _c.Message = InvalidOperation;
                    break;
            }
        }

        private void ViewOperation()
        {
            if (!CheckCustomerRecord(true)) return;
            _c.CopyFromCustomer(_dbOps.LastCustomer);
            _c.Message = InformationRetrieved;
        }

        private void DeleteOperation()
        {
            if (!CheckCustomerRecord(true) || !LastRecordMatch()) return;
            _dbOps.DeleteCustomer();
            _c.Message = CustomerDeleted;
        }

        private void ChangeOperation()
        {
            if (!CheckCustomerRecord(true) || !LastRecordMatch() || !IsNameValid() || !IsAddressValid() ||
                !IsMobileValid()) return;
            _c.CopyToCustomer(_dbOps.LastCustomer);
            _c.Message = InformationUpdated;
        }

        private void AddOperation()
        {
            if (!CheckCustomerRecord(false) || !IsNameValid() || !IsAddressValid() || !IsMobileValid()) return;
            var custRecord = new Customer();
            _c.CopyToCustomer(custRecord);
            _dbOps.AddCustomer(custRecord);
            _c.Message = CustomerAdded;
        }

        private bool LastRecordMatch()
        {
            if (_c.Number != _oldNumber)
                _c.Message = ViewFirst;
            return _c.Number == _oldNumber;
        }

        private bool CheckCustomerRecord(bool isPresent)
        {
            if (!IsNumberValid()) return false;
            if (_dbOps.CheckCustomerNumber(_c.Number) == !isPresent)
            {
                _c.Message = isPresent?CustomerNumberAbsent:CustomerNumberPresent;
                return false;                
            }
            return true;
        }

        private bool IsNumberValid()
        {
            if (_c.Number <= 0)
            {
                _c.Message = CustomerNumberInvalid;
                return false;
            }
            return EnsureValidString(_c.Number.ToString(), CView.NumberLength, CustomerNumberInvalid);
        }

        private bool IsNameValid()
        {
            return EnsureValidString(_c.Name, CView.NameLength, NameInvalid);
        }

        private bool IsAddressValid()
        {
            return EnsureValidString(_c.Address, CView.AddressLength, AddressInvalid);
        }

        private bool IsMobileValid()
        {
            return EnsureValidString(_c.Mobile, CView.MobileLength, MobileInvalid) &&
                   EnsureDigitsOnly(_c.Mobile, MobileInvalid);
        }

        private bool EnsureValidString(string s, int maxLength, string errorMessage)
        {
            bool error = string.IsNullOrEmpty(s) || s.Length > maxLength;
            if (error) _c.Message = errorMessage;
            return !error;
        }

        private bool EnsureDigitsOnly(string s, string errorMessage)
        {
            if (Regex.IsMatch(s, @"^\d+$")) return true;
            _c.Message = errorMessage;
            return false;
        }
    }
}