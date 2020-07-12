using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace alza
{
    public class PageObject
    {
        public IWebDriver driver;

        public PageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void goToUrl(string url) {
            driver.Navigate().GoToUrl(url);
        }

        public IWebElement findElement(By by) {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(15))
                .Until(ExpectedConditions.ElementIsVisible(by));
        }

        public ReadOnlyCollection<IWebElement> findElements(By by) {
            waitForElement(by);
            return driver.FindElements(by);
        }

        private void waitForElement(By by) {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.ElementIsVisible(by));
        }



    }
}