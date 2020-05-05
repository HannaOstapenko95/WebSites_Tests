using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;

namespace DemoWebApp.Tests
{
    public class ApplicationConfirmationPage
    {
        private readonly IWebDriver _driver;

		[FindsBy(How = How.Id, Using  = "firstName")]
		private IWebElement _firstName;
		public  ApplicationConfirmationPage (IWebDriver driver)
		{
			_driver = driver;
			PageFactory.InitElements(_driver, this);
		}
		//public string FirstName => _driver.FindElement(By.Id("firstName")).Text;
		public string FirstName => _firstName.Text;
	}
}
