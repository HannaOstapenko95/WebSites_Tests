using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSite
{
    public class SignUpPage : PageObject
    {
        // private readonly IWebDriver _driver;
        private const string url = @"http://www.kimschiller.com/page-object-pattern-tutorial/index.html";
        [FindsBy(How = How.Id, Using = "firstname")]
        private IWebElement firstName;

        [FindsBy(How = How.Id, Using = "lastname")]
        private IWebElement lastName;

        [FindsBy(How = How.Id, Using = "address")]
        public IWebElement address;

        [FindsBy(How = How.Id, Using = "zipcode")]
        private IWebElement zipCode;

        [FindsBy(How = How.Id, Using = "signup")]
        private IWebElement submitButton;

        public SignUpPage(IWebDriver driver) : base(driver) { }      
            
        public static SignUpPage NavigateTo (IWebDriver driver)
        {
            driver.Navigate().GoToUrl(url);
            return new SignUpPage(driver);
        }
        public void enterName(String firstName, String lastName)
        {
            this.firstName.Clear();
            this.firstName.SendKeys(firstName);

            this.lastName.Clear();
            this.lastName.SendKeys(lastName);
        }

        public void enterAddress(String address, String zipCode)
        {
            this.address.Clear();
            this.address.SendKeys(address);

            this.zipCode.Clear();
            this.zipCode.SendKeys(zipCode);
        }

        public ReceiptPage submit()
        {
            submitButton.Click();
            return new ReceiptPage(driver);
        }
    }
}
