using EccomerceAutomationTest.specs.StepsDefinition;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;
namespace EccomerceAutomationTest.specs.POM
{
    public class HomePage : IClassFixture<WebDriverDefinition>
    {
        public WebDriverDefinition driverBase;
        public IWebDriver driver;
        public HomePage(WebDriverDefinition driverBase)
        {
            driver = driverBase.getDriver();
        }
        public string GetPageTitle()
        {
            return driver.Title;
        }
        public void setBrandFilter(string brand)
        {
            SelectElement dropdownBand = new SelectElement(driver.FindElement(By.Id("CatalogModel_BrandFilterApplied")));
            dropdownBand.SelectByText(brand);
        }
        public void setTypeFilter(string type)
        {
            SelectElement dropdownType = new SelectElement(driver.FindElement(By.Id("CatalogModel_TypesFilterApplied")));
            dropdownType.SelectByText(type);
        }
        public string getBrandFilter()
        {
            SelectElement brandFilter = new SelectElement(driver.FindElement(By.Id("CatalogModel_BrandFilterApplied")));
            return brandFilter.SelectedOption.Text;
        }
        public string getTypeFilter()
        {
            SelectElement typeFilter = new SelectElement(driver.FindElement(By.Id("CatalogModel_TypesFilterApplied")));
            return typeFilter.SelectedOption.Text;
        }
        public void navigateToBasket()
        {
            driver.FindElement(By.ClassName("esh-basketstatus")).Click();
        }
    }
}