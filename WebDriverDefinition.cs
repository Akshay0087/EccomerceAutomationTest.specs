using OpenQA.Selenium;
namespace EccomerceAutomationTest.specs.StepsDefinition
{
    public class WebDriverDefinition
    {
        public readonly IWebDriver driver;
        protected WebDriverDefinition(IWebDriver _driver)
        {
            driver = _driver;
        }
        public IWebDriver getDriver()
        {
            return driver;
        }
    }
}