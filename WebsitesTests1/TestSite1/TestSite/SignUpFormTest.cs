using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestSite
{
    public class SignUpFormTest : FunctionalTest
    {
        [Fact]
        public void signUp()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://www.kimschiller.com/page-object-pattern-tutorial/index.html");

            SignUpPage signUpPage = new SignUpPage(driver);
           //Assert.IsTrue(signUpPage.);
            //assertTrue(signUpPage.isInitialized());

            signUpPage.enterName("First", "Last");
            signUpPage.enterAddress("123 Street", "12345");

            ReceiptPage receiptPage = signUpPage.submit();
            //assertTrue(receiptPage.isInitialized());

            Assert.Equal("Thank you!", receiptPage.confirmationHeader());
            driver.Close();
        }
    }
}
