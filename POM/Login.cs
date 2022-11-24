using EccomerceAutomationTest.specs.StepsDefinition;
using OpenQA.Selenium;
using System.Linq;
using Xunit;
namespace EccomerceAutomationTest.specs.POM
{
    public class Login : IClassFixture<WebDriverDefinition>
    {
        private readonly WebDriverDefinition driverBase;
        public IWebDriver Driver;
        public
        const string homePageURL = "https://localhost:44315/";

        By loginXpath = By.XPath("/html/body/div/header/div/article/section[2]/div/section/div/a");
        By emailInputId = By.Id("Input_Email");
        By passwordInputName = By.Name("Input.Password");
        By loginAuthenticateXpath = By.XPath("/html/body/div/div/div/div/section/form/div[5]/button");
        By currentUserEmailXpath = By.XPath("/ html / body / div / header[1] / div / article / section[2] / div / form / section[1] / div");
        By errorListSelector = By.CssSelector("body > div > div > div > div > section > form > div.text-danger.validation-summary-errors > ul > li");
        public Login(WebDriverDefinition driverBase)
        {
            this.Driver = driverBase.getDriver();
        }
        public void NavigateToHomePage()
        {
            Driver.Navigate().GoToUrl(homePageURL);
        }
        public string GetPageTitle()
        {
            return Driver.Title;
        }
        public void clickLoginInNavBar()
        {
            Driver.FindElement(loginXpath).Click();
        }
        public void setEmail(string email)
        {
            Driver.FindElement(emailInputId).SendKeys(email);
        }
        public void setPassword(string password)
        {
            Driver.FindElement(passwordInputName).SendKeys(password);
        }
        public void clickLoginForAuthentication()
        {
            Driver.FindElement(loginAuthenticateXpath).Click();
        }
        public string getCurrentUserEmail()
        {
            return Driver.FindElement(currentUserEmailXpath).Text;
        }
        public string getErrorMsg()
        {
            var errorList = Driver.FindElements(errorListSelector);
            return errorList.First().Text;
        }
    }
}