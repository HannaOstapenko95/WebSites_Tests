using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCards.UITests.PageObjectModels
{
    public class HomePage : Page
    {
        private readonly IWebDriver Driver;
        protected override string PageUrl => "http://localhost:44108/";
        protected override string PageTitle => "Home Page - Credit Cards";
        public string GenerationToken => Driver.FindElement(By.Id("GenerationToken")).Text;
        public bool IsCookieMessagePresent => Driver.FindElements(By.Id("CookiesBeingUsed")).Any();
        public void ClickContactFooterLink() => Driver.FindElement(By.Id("ContactFooter")).Click(); 
        public void ClickLiveChatFooterLink() => Driver.FindElement(By.Id("LiveChat")).Click();
        public void ClickLearnAboutUsLink() => Driver.FindElement(By.Id("LearnAboutUs")).Click();
        public ApplicationPage ClickApplyLowRateLink()
        {
            Driver.FindElement(By.Name("ApplyLowRate")).Click();
            return new ApplicationPage(Driver);
        }
        public void WaitForEasyApplicationCarouselPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(11));
            wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));
        }
        public ApplicationPage ClickApplyEasyApplicationLink()
        {
            //Driver.FindElement(By.LinkText("Easy: Apply Now!")).Click();
            string script = @"document.evaluate('//a[text()[contains(.,\'Easy: Apply Now!\')]]', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.click();";
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript(script);
            return new ApplicationPage(Driver);
        }
        public HomePage (IWebDriver driver)
        {
            Driver = driver;
        }
        public ReadOnlyCollection<(string name, string interestRate)> Products
        {
            get
            {
                var products = new List<(string name, string interestRate)>();
                var productCells =  Driver.FindElements(By.TagName("td"));
                for (int i = 0; i < productCells.Count - 1; i+=2)
                {
                    string name = productCells[i].Text;
                    string interestRate = productCells[i + 1].Text;
                    products.Add((name, interestRate));
                }
                return products.AsReadOnly();
            }
        }
        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            EnsurePageLoaded();
        }
        public void EnsurePageLoaded(bool onlyCheckUrlStartsWithExpectedText = true)
        {
            //bool pageHasLoaded = (Driver.Url == PageUrl) && (Driver.Title == PageTitle);
            bool urlIsCorrect;
            if (onlyCheckUrlStartsWithExpectedText)
            {
                urlIsCorrect = Driver.Url.StartsWith(PageUrl);
            }
            else
            {
                urlIsCorrect = Driver.Url == PageUrl;
            }
            bool pageHasLoaded = urlIsCorrect && (Driver.Title == PageTitle);
            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = '{Driver.Url}' Page Source: \r\n {Driver.PageSource}");
            }
        }
    }
}
