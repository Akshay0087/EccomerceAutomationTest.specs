using EccomerceAutomationTest.specs.POM;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace EccomerceAutomationTest.specs.StepsDefinition
{
    [Binding]
    public class LoginStepDefinitions : IClassFixture<WebDriverDefinition>
    {
        public WebDriverDefinition driverBase;
        public Login login;
        
        public LoginStepDefinitions(WebDriverDefinition driverBase)
        {
            this.driverBase = driverBase;
            this.login = new Login(driverBase);
        }
        
        public void NavigateToPage(string URL)
        {
            driverBase.driver.Navigate().GoToUrl(URL);
        }

        [Given(@"User is at the Home Page")]
        public void GivenUserIsAtTheHomePage()
        {
            login.NavigateToHomePage();
            Assert.Equal("Catalog - Microsoft.eShopOnWeb", login.GetPageTitle());
        }

        [Given(@"User Navigates to Login Page")]
        public void GivenUserNavigatesToLoginPage()
        {
            login.clickLoginInNavBar();
            Assert.Equal("Log in - Microsoft.eShopOnWeb", login.GetPageTitle());
        }

        [When(@"User enters Email_Address ""([^""]*)"" and Password ""([^""]*)""")]
        public void WhenUserEntersEmail_AddressAndPassword(String email, string password)
        {
            login.setEmail(email);
            login.setPassword(password);
        }

        [When(@"User clicks on the LogIn button")]
        public void WhenUserClicksOnTheLogInButton()
        {
            login.clickLoginForAuthentication();
        }

        [Then(@"User is Redirected to Home Page")]
        public void ThenUserIsRedirectedToHomePage()
        {
            login.NavigateToHomePage();
            Assert.Equal("Catalog - Microsoft.eShopOnWeb", login.GetPageTitle());
        }

        [Then(@"Users' Email_Address ""([^""]*)"" is displayed in navigation bar")]
        public void ThenUsersEmail_AddressIsDisplayedInNavigationBar(string initialEmail)
        {
            Assert.Equal(initialEmail, login.getCurrentUserEmail());
        }

        [Then(@"Invalid credentials error message is displayed")]
        public void ThenInvalidCredentialsErrorMessageIsDisplayed()
        {
            Assert.Equal("Invalid login attempt.",login.getErrorMsg());
        }
    }
}
