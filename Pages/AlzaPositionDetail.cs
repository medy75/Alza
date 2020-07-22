using OpenQA.Selenium;
using log4net;
using System.Collections.ObjectModel;

namespace alza
{
    
    class AlzaPositionDetail : PageObject
    {
        
        public AlzaPositionDetail(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement titleElement() {
            return findElement(By.CssSelector("div.content-container > h1"));
        }

        private IWebElement firstPersonElement() {
            return findElement(By.CssSelector("div.card.people:nth-child(1) p.subtitle"));
        }

        private IWebElement secondPersonElement() {
            return findElement(By.CssSelector("div.card.people:nth-child(2) p.subtitle"));
        }

        public string title() {
            return titleElement().Text;
        }

        public string firstPersonName(){
            return firstPersonElement().Text;
        }

        public string secondPersonName(){
            return secondPersonElement().Text;
        }


    }
}