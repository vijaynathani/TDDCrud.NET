using System;
using BDD.Common;
using TechTalk.SpecFlow;

namespace BDD.CustomerModule.Steps
{
    [Binding]
    public class CustomerValidity
    {
        [When(@"I try to add or change Customer values")]
        public void WhenITryToAddOrChangeCustomerValues()
        {
        }
        
        [Then(@"Empty name, address and mobile should be treated as invalid")]
        public void ThenEmptyNameAddressAndMobileShouldBeTreatedAsInvalid()
        {
            TestFunction.EnsureTestMethodPresent(typeof(DomainTest.Cust.CustomerValidatorIndividualItemsTest), "NameEmpty");
            TestFunction.EnsureTestMethodPresent(typeof(DomainTest.Cust.CustomerValidatorIndividualItemsTest), "AddressEmpty");
            TestFunction.EnsureTestMethodPresent(typeof(DomainTest.Cust.CustomerValidatorIndividualItemsTest), "MobileEmpty");
        }

        [Then(@"Mobile number having non-digit characters should also be treated as invalid\.")]
        public void ThenMobileNumberHavingNon_DigitCharactersShouldAlsoBeTreatedAsInvalid_()
        {
            TestFunction.EnsureTestMethodPresent(typeof(DomainTest.Cust.CustomerValidatorIndividualItemsTest), "MobileWithAlphaCharacters");
        }

    }
}
