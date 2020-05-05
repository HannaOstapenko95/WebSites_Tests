using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSite
{
    public class ReceiptPage : PageObject
    {
        [FindsBy(How = How.TagName, Using = "h1")]
        private IWebElement header;

        public ReceiptPage (IWebDriver driver) : base(driver) { }
        
            //super(driver);
            
        

        public String confirmationHeader()
        {
            return header.Text;
        }
    }
}
