using CreditCards.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CreditCards.UITests
{
    [Trait("Category", "Application")]
    public class CreditCardApplicationShould : IClassFixture<ChromeDriverFixture>
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string ApplyUrl = "http://localhost:44108/Apply";

        public readonly ChromeDriverFixture ChromeDriverFixture;

        //private readonly ITestOutputHelper output;
        //public CreditCardApplicationShould(ITestOutputHelper output)
        //{
        //    this.output = output;
        //}
        public CreditCardApplicationShould(ChromeDriverFixture chromeDriverFixture)
        {
            ChromeDriverFixture = chromeDriverFixture;
            ChromeDriverFixture.Driver.Manage().Cookies.DeleteAllCookies();
            ChromeDriverFixture.Driver.Navigate().GoToUrl("about:blank");
        }
        [Fact]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            //using (IWebDriver driver = new ChromeDriver())
            //{
            // var homePage = new HomePage(driver);
            var homePage = new HomePage(ChromeDriverFixture.Driver);
            homePage.NavigateTo();
                ApplicationPage applicationPage = homePage.ClickApplyLowRateLink();
                applicationPage.EnsurePageLoaded();

                //driver.Navigate().GoToUrl(HomeUrl);
                //DemoHelper.Pause();
                //IWebElement applyLink = driver.FindElement(By.Name("ApplyLowRate"));
                //applyLink.Click();
                //DemoHelper.Pause();
                //Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                //Assert.Equal(ApplyUrl, driver.Url);
          //  }
        }
        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            //using (IWebDriver driver = new ChromeDriver())
            //{
                var homePage = new HomePage(ChromeDriverFixture.Driver);
                homePage.NavigateTo();
                homePage.WaitForEasyApplicationCarouselPage();
                ApplicationPage applicationPage = homePage.ClickApplyEasyApplicationLink();
                applicationPage.EnsurePageLoaded();


                //driver.Navigate().GoToUrl(HomeUrl);
                ////DemoHelper.Pause(11000);
                //DemoHelper.Pause();
                //IWebElement carouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                //carouselNext.Click();
                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                //IWebElement applyLink = wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));
                //applyLink.Click();
                ////DemoHelper.Pause(1000);
                ////IWebElement applyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                ////applyLink.Click();
                //DemoHelper.Pause();
                //Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                //Assert.Equal(ApplyUrl, driver.Url);
           // }
        }
        [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                Func<IWebDriver, IWebElement> findEnabledAndVisible = delegate (IWebDriver d)
                {
                    var e = d.FindElement(By.ClassName("customer-service-apply-now"));
                    if (e is null) throw new NotFoundException();
                    if (e.Enabled && e.Displayed) return e;
                    throw new NotFoundException();
                };
                //Implicit wait
                //output.WriteLine($"{DateTime.Now.ToLongTimeString()} Setting implicit wait.");
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(35);
                //output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to '{HomeUrl}'");
                driver.Navigate().GoToUrl(HomeUrl);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));

                ////DemoHelper.Pause(11000);
                //DemoHelper.Pause();
                //IWebElement carouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                //carouselNext.Click();
                //DemoHelper.Pause(1000);
                //carouselNext.Click();
                //DemoHelper.Pause(1000);
                //output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element");
                //IWebElement applyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("customer-service-apply-now")));
                IWebElement applyLink = wait.Until(findEnabledAndVisible);
                //output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element displayed={applyLink.Displayed} Enabled={applyLink.Enabled}");
                //output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");
                applyLink.Click();
                DemoHelper.Pause();
                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();
                IWebElement randomGreetingApplyLink = driver.FindElement(By.PartialLinkText("- Apply Now!"));
                randomGreetingApplyLink.Click();
                DemoHelper.Pause();
                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting_Using_XPATH()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();
                IWebElement randomGreetingApplyLink = 
                  //  driver.FindElement(By.XPath("/html/body/div/div[4]/div/p/a")); //absolute XPath
                driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]")); //relativeXPath
                randomGreetingApplyLink.Click();
                DemoHelper.Pause();
                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication_Prebuilt_Conditions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                driver.Manage().Window.Minimize();
                DemoHelper.Pause();
                //explicit waits
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(11));
                IWebElement applyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));
                applyLink.Click();
                DemoHelper.Pause();
                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
        [Fact]
        public void BeSubmittedWhenValid()
        {
            const string FirstName = "Sarah";
            const string LastName = "Smith";
            const string Number = "123456-A";
            const string Age = "18";
            const string Income = "50000";
            using (IWebDriver driver = new ChromeDriver())
            {
                var applicationPage = new ApplicationPage(driver);
                applicationPage.NavigateTo();

                applicationPage.EnterFirstName(FirstName);
                applicationPage.EnterLastName(LastName);
                applicationPage.EnterFrequentFlyerNumber(Number);
                applicationPage.EnterAge(Age);
                applicationPage.EnterGrossAnnualIncome(Income);
                applicationPage.ChooseMaritalStatusSingle();
                applicationPage.ChooseBusinessSourceTV();
                applicationPage.AcceptTerms();
                ApplicationCompletePage applicationCompletePage = applicationPage.SubmitApplication();

                applicationCompletePage.EnsurePageLoaded();
                Assert.Equal("ReferredToHuman", applicationCompletePage.Decision);
                Assert.NotEmpty(applicationCompletePage.ReferenceNumber);
                Assert.Equal($"{FirstName} {LastName}", applicationCompletePage.FullName);
                Assert.Equal(Age, applicationCompletePage.Age);
                Assert.Equal(Income, applicationCompletePage.Income);
                Assert.Equal("Single", applicationCompletePage.RelationshipStatus);
                Assert.Equal("TV", applicationCompletePage.BusinessSource);


                //driver.Navigate().GoToUrl(ApplyUrl);
                //driver.FindElement(By.Id("FirstName")).SendKeys("Sarah");
                //DemoHelper.Pause();
                //driver.FindElement(By.Id("LastName")).SendKeys("Smith");
                //DemoHelper.Pause();
                //driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("123456-A");
                //DemoHelper.Pause();
                //driver.FindElement(By.Id("Age")).SendKeys("18");
                //DemoHelper.Pause();
                //driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");
                //DemoHelper.Pause(5000);
                //driver.FindElement(By.Id("Single")).Click();
                //DemoHelper.Pause();
                //IWebElement businessSourceSelectElement = driver.FindElement(By.Id("BusinessSource"));
                //SelectElement businessSource = new SelectElement(businessSourceSelectElement);
                //Assert.Equal("I'd Rather Not Say", businessSource.SelectedOption.Text);
                //foreach (IWebElement option in businessSource.Options)
                //{
                //    output.WriteLine($"Value: {option.GetAttribute("value")} Text: {option.Text}");
                //}
                //Assert.Equal(5, businessSource.Options.Count);
                //businessSource.SelectByValue("Email");
                //DemoHelper.Pause();
                //businessSource.SelectByText("Internet Search");
                //DemoHelper.Pause();
                //businessSource.SelectByIndex(4);
                //DemoHelper.Pause();
                //driver.FindElement(By.Id("TermsAccepted")).Click();
                //DemoHelper.Pause();
                //driver.FindElement(By.Id("Single")).Submit();
                //DemoHelper.Pause();
                ////driver.FindElement(By.Id("SubmitApplication")).Click();
                ////DemoHelper.Pause();
                //Assert.StartsWith("Application Complete", driver.Title);
                //Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                //Assert.NotEmpty(driver.FindElement(By.Id("ReferenceNumber")).Text);
                //Assert.Equal("Sarah Smith", driver.FindElement(By.Id("FullName")).Text);
                //Assert.Equal("18", driver.FindElement(By.Id("Age")).Text);
                //Assert.Equal("50000", driver.FindElement(By.Id("Income")).Text);
                //Assert.Equal("Single", driver.FindElement(By.Id("RelationshipStatus")).Text);
                //Assert.Equal("TV", driver.FindElement(By.Id("BusinessSource")).Text);
            }
        }
             [Fact]
        public void BeSubmittedWhenValidationErrorsCorrected()
        {
            const string firstName = "Sarah";
            const string invalidAge = "17";
            const string validAge = "18";
            using (IWebDriver driver = new ChromeDriver())
            {
                var applicationPage = new ApplicationPage(driver);
                applicationPage.NavigateTo();

                applicationPage.EnterFirstName(firstName);
                // Don't enter lastname
                applicationPage.EnterFrequentFlyerNumber("123456-A");
                applicationPage.EnterAge(invalidAge);
                applicationPage.EnterGrossAnnualIncome("50000");
                applicationPage.ChooseMaritalStatusSingle();
                applicationPage.ChooseBusinessSourceTV();
                applicationPage.AcceptTerms();
                applicationPage.SubmitApplication();

                // Assert that validation failed                                
                Assert.Equal(2, applicationPage.ValidationErrorMessages.Count);
                Assert.Contains("Please provide a last name", applicationPage.ValidationErrorMessages);
                Assert.Contains("You must be at least 18 years old", applicationPage.ValidationErrorMessages);

                // Fix errors
                applicationPage.EnterLastName("Smith");
                applicationPage.ClearAge();
                applicationPage.EnterAge(validAge);

                // Resubmit form
                ApplicationCompletePage applicationCompletePage = applicationPage.SubmitApplication();

                // Check form submitted
                applicationCompletePage.EnsurePageLoaded(); 

                //driver.Navigate().GoToUrl(ApplyUrl);
                //driver.FindElement(By.Id("FirstName")).SendKeys(firstName);
                //DemoHelper.Pause();
                ////Do not enter last name
                //driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("123456-A");
                //DemoHelper.Pause();
                //driver.FindElement(By.Id("Age")).SendKeys(invalidAge);
                //DemoHelper.Pause();
                //driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");
                //DemoHelper.Pause(5000);
                //driver.FindElement(By.Id("Single")).Click();
                //DemoHelper.Pause();
                //IWebElement businessSourceSelectElement = driver.FindElement(By.Id("BusinessSource"));
                //SelectElement businessSource = new SelectElement(businessSourceSelectElement);
                //Assert.Equal("I'd Rather Not Say", businessSource.SelectedOption.Text);
                //foreach (IWebElement option in businessSource.Options)
                //{
                //    output.WriteLine($"Value: {option.GetAttribute("value")} Text: {option.Text}");
                //}
                //Assert.Equal(5, businessSource.Options.Count);
                //businessSource.SelectByValue("Email");
                //DemoHelper.Pause();
                //businessSource.SelectByText("Internet Search");
                //DemoHelper.Pause();
                //businessSource.SelectByIndex(4);
                //DemoHelper.Pause();
                //driver.FindElement(By.Id("TermsAccepted")).Click();
                //DemoHelper.Pause();
                //driver.FindElement(By.Id("Single")).Submit();
                //DemoHelper.Pause();
                ////driver.FindElement(By.Id("SubmitApplication")).Click();
                ////DemoHelper.Pause();

                ////Validation Asserts
                //var validationErrors = driver.FindElements(By.CssSelector(".validation-summary-errors > ul > li"));
                //Assert.Equal(2, validationErrors.Count);
                //Assert.Equal("Please provide a last name", validationErrors[0].Text);
                //Assert.Equal("You must be at least 18 years old", validationErrors[1].Text);

                ////Fix errors
                //driver.FindElement(By.Id("LastName")).SendKeys("Smith");
                //driver.FindElement(By.Id("Age")).Clear();
                //driver.FindElement(By.Id("Age")).SendKeys(validAge);

                ////Resubmit form
                //driver.FindElement(By.Id("SubmitApplication")).Click();
                ////Check form submitted
                //Assert.StartsWith("Application Complete", driver.Title);
                //Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                //Assert.NotEmpty(driver.FindElement(By.Id("ReferenceNumber")).Text);
                //Assert.Equal("Sarah Smith", driver.FindElement(By.Id("FullName")).Text);
                //Assert.Equal("18", driver.FindElement(By.Id("Age")).Text);
                //Assert.Equal("50000", driver.FindElement(By.Id("Income")).Text);
                //Assert.Equal("Single", driver.FindElement(By.Id("RelationshipStatus")).Text);
                //Assert.Equal("TV", driver.FindElement(By.Id("BusinessSource")).Text);
            }
        }
    }
}
