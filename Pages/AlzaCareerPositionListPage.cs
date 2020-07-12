using OpenQA.Selenium;
using log4net;
using System.Collections.ObjectModel;

namespace alza
{
    class AlzaCareerPositionListPage : PageObject
    {
        
        public AlzaCareerPositionListPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement jobList()
        {
            return findElement(By.CssSelector("job-offer-list div.container a"));
        }

        private IWebElement headerTitle() {
            return findElement(By.CssSelector("job-detail-header h1"));
        }

        public void waitForJobListLoad()
        {
            jobList();
        }

        public string titleText() {
            return headerTitle().Text;
        }

        public ReadOnlyCollection<IWebElement> positionsList() {
            return findElements(By.CssSelector("h3.job-title"));
        }
    }
}