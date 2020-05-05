using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using TechTalk.SpecFlow;
using TestSite;
using Xunit;

namespace TestSite
{
    [Binding]
    public class RegistrationSteps
    {
        IWebDriver driver;
        private SignUpPage _signUpPage;
        private ReceiptPage _receiptPage;
       [Given]
        public void Given_I_am_on_page_for_registration()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            _signUpPage = SignUpPage.NavigateTo(driver);
            //SignUpPage signUpPage = new SignUpPage(driver);
        }
        

        [Given(@"I have entered a Firstname as (.*) and a Secondname as (.*)")]
        public void GivenIHaveEnteredAFirstnameAsHannaAndASecondnameAsOstapenko(string nameFirst, string nameSecond)
        {
            _signUpPage.enterName(nameFirst, nameSecond);
        }

        [Given(@"I have entered Address as (.*) and Zipcode as (.*)")]
        public void GivenIHaveEnteredAddressAndZipcode(string address, string zipcode)
        {
            _signUpPage.enterAddress(address, zipcode);
        }

        // public void Given_I_have_entered_Address_and_Zipcode(string address, string zipcode)
        //{
        //  _signUpPage.enterAddress(address, zipcode);
        //}



        [When]
        public void When_I_press_Sign_up()
        {
            _receiptPage = _signUpPage.submit();
        }
        
        [Then]
        public void Then_I_see_Thank_you_on_the_page()
        {
            _receiptPage = new ReceiptPage(driver);
            Assert.Equal("Thank you!", _receiptPage.confirmationHeader());
            driver.Close();
        }
    }
}
