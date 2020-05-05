using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace TestSite
{
    public class FunctionalTest
    {
        protected static IWebDriver driver;
        [BeforeScenario]
        public static void SetUp()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts();
           // ImplicitWait(10, TimeSpan.Equals);
        }

        [AfterScenario]
        public void DisposeWebDriver()
        {
            driver.Dispose();
        }
    }
}
