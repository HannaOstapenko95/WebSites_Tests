using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace DemoWebApp.Tests
{
    [Binding]
    public class LoanApplicationSteps
    {
        private IWebDriver _driver;
        private LoanApplicationPage _loanApplicationPage;
        private ApplicationConfirmationPage _confirmationPage;
        [Given(@"I am on the loan application screen")]
        public void GivenIAmOnTheLoanApplicationScreen()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Window.Maximize();
            // _driver.Navigate().GoToUrl("http://localhost:40077/Home/StartLoanApplication");
            _loanApplicationPage = LoanApplicationPage.NavigateTo(_driver);
        }
        
        //[Given(@"I enter a first name of Hanna")]
        [Given(@"I enter a first name of (.*)")]
        //public void GivenIEnterAFirstNameOfHanna()
         public void GivenIEnterAFirstNameOf(string firstName)
        {
            //IWebElement firstNameInput = _driver.FindElement(By.Id("FirstName"));
            //firstNameInput.SendKeys("Hanna");
            //firstNameInput.SendKeys(firstName);
            _loanApplicationPage.FirstName = firstName;
        }

        //[Given(@"I enter a second name as Ostapenko")]
        [Given(@"I enter a second name as (.*)")]
        //public void GivenIEnterASecondNameAsOstapenko()
        public void GivenIEnterASecondNameAs(string secondName)
        {
            //_driver.FindElement(By.Id("LastName")).SendKeys("Ostapenko");
            // _driver.FindElement(By.Id("LastName")).SendKeys(secondName);
            _loanApplicationPage.SecondName = secondName;
        }
        
        [Given(@"I select that I have an existing loan account")]
        public void GivenISelectThatIHaveAnExistingLoanAccount()
        {
            //_driver.FindElement(By.Id("Loan")).Click();
            _loanApplicationPage.SelectExistingLoan();
        }
        
        [Given(@"I confirm my acceptance of the terms and conditions")]
        public void GivenIConfirmMyAcceptanceOfTheTermsAndConditions()
        {
            // _driver.FindElement(By.Name("TermsAcceptance")).Click();
            _loanApplicationPage.AcceptTermsAndConditions();
        }
        
        [When(@"I submit my application")]
        public void WhenISubmitMyApplication()
        {
            // _driver.FindElement(By.CssSelector(".btn.btn-primary")).Click();
            _confirmationPage = _loanApplicationPage.SubmitApplication();
        }
        
        [Then(@"I should see the application complete confirmation for Hanna")]
        public void ThenIShouldSeeTheApplicationCompleteConfirmationForHanna()
        {
            /* IWebElement confirmationNameSpan = _driver.FindElement(By.Id("firstName"));
             string confirmationName = confirmationNameSpan.Text;
             Assert.Equal("Hanna", confirmationName);*/
            Assert.Equal("Hanna", _confirmationPage.FirstName);
        }
        [Then(@"I should see an error message telling me I must the accept the terms and conditions")]
        public void ThenIShouldSeeAnErrorMessageTellingMeIMustTheAcceptTheTermsAndConditions()
        {
            IWebElement errorElement = _driver.FindElement(By.CssSelector("div.validation-summary-errors ul li"));
            //Assert.Equal("You must accept the terms and conditions", errorElement.Text);
            Assert.Equal("You must accept the terms and conditions", _loanApplicationPage.ErrorText);
        }


        [AfterScenario]
        public void DisposeWebDriver()
        {
            _driver.Dispose();
        }
    }
}
