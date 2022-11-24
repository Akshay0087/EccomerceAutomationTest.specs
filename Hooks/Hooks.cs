using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using TechTalk.SpecFlow;

namespace EccomerceAutomationTest.specs.POM
{
    [Binding]
    public sealed class Hooks
    {

        private readonly IObjectContainer _objectContainer;
        public IWebDriver driver;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario("Chrome")]
        public void BeforeChrome()
        {
            driver = new ChromeDriver();
            _objectContainer.RegisterInstanceAs(driver);
        }


        [BeforeScenario("Edge")]
        public void BeforeEdge()
        {
            driver = new EdgeDriver();
            _objectContainer.RegisterInstanceAs(driver);
        }

        [BeforeScenario]
        public void BeforeScenario()
        { 
            if (driver==null)
            {
                driver = new EdgeDriver();
                _objectContainer.RegisterInstanceAs(driver);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _objectContainer.Resolve<IWebDriver>();
            driver.Quit();
            driver.Dispose();
        }




    }

}