using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.Windows;
using CreditCards.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xunit;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string AboutUrl = "http://localhost:44108/Home/About";
        private const string HomeTitle = "Home Page - Credit Cards";
        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadHomePage()
        {
            using(IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
                //driver.Navigate().GoToUrl(HomeUrl);
                //driver.Manage().Window.Maximize();
                //DemoHelper.Pause();
                //driver.Manage().Window.Minimize();
                //DemoHelper.Pause();
                //driver.Manage().Window.Size = new System.Drawing.Size(300, 400);
                //DemoHelper.Pause();
                //driver.Manage().Window.Position = new System.Drawing.Point(1, 1);
                //DemoHelper.Pause();
                //driver.Manage().Window.Position = new System.Drawing.Point(50, 50);
                //DemoHelper.Pause();
                //driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
                //DemoHelper.Pause();
                //driver.Manage().Window.FullScreen();
                //DemoHelper.Pause(5000);
                //Assert.Equal(HomeTitle, driver.Title);
                //Assert.Equal(HomeUrl, driver.Url);
            }
        }
        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();
                driver.Navigate().Refresh();
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }
        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
                driver.Navigate().GoToUrl(HomeUrl);
                string initialToken = homePage.GenerationToken;
                driver.Navigate().GoToUrl(AboutUrl);
                driver.Navigate().Back();
                homePage.EnsurePageLoaded();
                string reloadedToken = homePage.GenerationToken;
                Assert.NotEqual(initialToken, reloadedToken);

                //driver.Navigate().GoToUrl(HomeUrl);
                //IWebElement generationTokenElement = 
                //    driver.FindElement(By.Id("GenerationToken"));
                //string initialToken = generationTokenElement.Text;
                //DemoHelper.Pause();
                //driver.Navigate().GoToUrl(AboutUrl);
                //DemoHelper.Pause();
                //driver.Navigate().Back();
                //Assert.Equal(HomeTitle, driver.Title);
                //Assert.Equal(HomeUrl, driver.Url);
                //string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;
                //Assert.NotEqual(initialToken, reloadedToken);
            }
        }
        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();
                driver.Navigate().Back();
                DemoHelper.Pause();
                driver.Navigate().Forward();
                DemoHelper.Pause();
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }
        [Fact]
        public void DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                //driver.Navigate().GoToUrl(HomeUrl);
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
                DemoHelper.Pause();
                //ReadOnlyCollection<IWebElement> tableCells = driver.FindElements(By.TagName("td"));
                //ReadOnlyCollection<IWebElement> tableCells =
                //    homePage.ProductCells;

                Assert.Equal("Easy Credit Card", homePage.Products[0].name);
                Assert.Equal("20% APR", homePage.Products[0].interestRate);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }
        [Fact]
        public void OpenContactFooterLinkInNewTab()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
                homePage.ClickContactFooterLink();
                ReadOnlyCollection<string> allTabs = driver.WindowHandles;
                string homePageTab = allTabs[0];
                string contactTab = allTabs[1];
                driver.SwitchTo().Window(contactTab);
                Assert.EndsWith("/Home/Contact", driver.Url);

                //driver.Navigate().GoToUrl(HomeUrl);
                //driver.FindElement(By.Id("ContactFooter")).Click();
                //DemoHelper.Pause();
                //ReadOnlyCollection<string> allTabs = driver.WindowHandles;
                //string homePageTab = allTabs[0];
                //string contactTab = allTabs[1];
                //driver.SwitchTo().Window(contactTab);
                //Assert.EndsWith("/Home/Contact", driver.Url);
            }
        }
        [Fact]
        public void AlertIfLiveChatClosed()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
                homePage.ClickLiveChatFooterLink();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
                Assert.Equal("Live chat is currently closed.", alert.Text);
                alert.Accept();

                //driver.Navigate().GoToUrl(HomeUrl);
                //driver.FindElement(By.Id("LiveChat")).Click();
                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                //IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
                //// IAlert alert = driver.SwitchTo().Alert();
                //Assert.Equal("Live chat is currently closed.", alert.Text);
                //DemoHelper.Pause();
                //alert.Accept();
                //DemoHelper.Pause();
            }
        }
        [Fact]
        public void NavigateToAboutUsWhenOkClicked()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
                homePage.ClickLearnAboutUsLink();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());
                alertBox.Accept();
                Assert.EndsWith("/Home/About", driver.Url);
            }
        }
        [Fact]
        public void NotNavigateToAboutUsWhenCancelClicked()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
                homePage.ClickLearnAboutUsLink();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());
                alertBox.Dismiss();
                homePage.EnsurePageLoaded();
                Assert.Equal(HomeTitle, driver.Title);

                //driver.Navigate().GoToUrl(HomeUrl);
                //Assert.Equal(HomeTitle, driver.Title);
                //driver.FindElement(By.Id("LearnAboutUs")).Click();
                //DemoHelper.Pause();
                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                //IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());
                //alertBox.Dismiss();
                //Assert.Equal(HomeTitle, driver.Title);
            }
        }
        [Fact]
        public void NotDisplayCookieUseMessage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
                driver.Manage().Cookies.AddCookie(new Cookie("acceptedCookies", "true"));
                driver.Navigate().Refresh();
                Assert.False(homePage.IsCookieMessagePresent);
                driver.Manage().Cookies.DeleteCookieNamed("acceptedCookies");
                driver.Navigate().Refresh();
                Assert.True(homePage.IsCookieMessagePresent);

                //driver.Navigate().GoToUrl(HomeUrl);
                //driver.Manage().Cookies.AddCookie(new Cookie("acceptedCookies", "true"));
                //driver.Navigate().Refresh();
                //var message = driver.FindElements(By.Id("CookiesBeingUsed"));
                //Assert.Empty(message);
                //Cookie cookieValue = driver.Manage().Cookies.GetCookieNamed("acceptedCookies");
                //Assert.Equal("true", cookieValue.Value);
                //driver.Manage().Cookies.DeleteCookieNamed("acceptedCookies");
                //driver.Navigate().Refresh();
                //Assert.NotNull(driver.FindElement(By.Id("CookiesBeingUsed")));
            }
        }
        [Fact]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void RenderAboutPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutUrl);
                ITakesScreenshot screenShotDriver = (ITakesScreenshot)driver;
                Screenshot screenshot = screenShotDriver.GetScreenshot();
                screenshot.SaveAsFile("aboutpage.bmp", ScreenshotImageFormat.Bmp);
                FileInfo file = new FileInfo("aboutpage.bmp");
                Approvals.Verify(file);
            }
        }
    }
}
