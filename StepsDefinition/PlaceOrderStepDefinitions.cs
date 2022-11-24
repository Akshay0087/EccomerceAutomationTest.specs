using EccomerceAutomationTest.specs.POM;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Xunit;

namespace EccomerceAutomationTest.specs.StepsDefinition
{
    [Binding]
    public class PlaceOrderStepDefinitions
    {
        private readonly WebDriverDefinition driverBase;
        private readonly Login Login;
        private readonly HomePage HomePage;
        private readonly BasketPage BasketPage;
        private readonly OrderPage OrderPage;
        Dictionary<string,string> ItemOrder;

        public PlaceOrderStepDefinitions(WebDriverDefinition _driverBase,Login login, HomePage homePage, BasketPage basketPage, OrderPage orderPage)
        {      
            this.driverBase = _driverBase;
            this.Login = login;
            this.HomePage = homePage;
            this.BasketPage = basketPage;
            this.OrderPage = orderPage;
            OrderPage = orderPage;
        }

        [Given(@"User is logs in with Email_Address ""([^""]*)"" and Password ""([^""]*)""")]
        public void GivenUserIsLogsInWithEmail_AddressAndPassword(string email, string password)
        {
            Login.NavigateToHomePage();
            Login.clickLoginInNavBar();
            Login.setEmail(email);
            Login.setPassword(password);
            Login.clickLoginForAuthentication();
            Login.NavigateToHomePage();
            Assert.Equal(Login.getCurrentUserEmail(),email);
        }

        [Given(@"User is on Homepage")]
        public void GivenUserIsOnHomepage()
        {
            Assert.Equal("Catalog - Microsoft.eShopOnWeb", Login.GetPageTitle());
        }

        [When(@"User searches in Brand ""([^""]*)"" and Type ""([^""]*)""")]
        public void WhenUserSearchesInBrandAndType(string brand, string type)
        {
            HomePage.setBrandFilter(brand);
            HomePage.setTypeFilter(type);
            Assert.Equal(brand, HomePage.getBrandFilter());
            Assert.Equal(type, HomePage.getTypeFilter());
        }

        [When(@"the order is placed")]
        public void WhenTheOrderIsPlaced()
        {
            Assert.Equal("Catalog - Microsoft.eShopOnWeb", HomePage.GetPageTitle());
            HomePage.navigateToBasket();
            Assert.Equal("Basket - Microsoft.eShopOnWeb", driverBase.driver.Title);
            BasketPage.clickOnCheckout();
            Assert.Equal("Checkout - Microsoft.eShopOnWeb", BasketPage.GetPageTitle());
            BasketPage.clickOnPay();
        }

        [Then(@"a confirmation message is displayed")]
        public void ThenAConfirmationMessageIsDisplayed()
        {
            Assert.Equal("Thanks for your Order!", BasketPage.GetConfirmationMessage());
        }

        [When(@"User selects items with quantities")]
        public void WhenUserSelectsItemsWithQuantities(Table table)
        {
            BasketPage.selectUserItem(table);
            ItemOrder = BasketPage.ToDictionary(table);
        }

        [Then(@"the items appear in my orders section")]
        public void ThenTheItemsAppearInMyOrdersSection()
        {
            OrderPage.checkOrderedProduct(ItemOrder);
        }

        [Then(@"the items are ordered with a quatity of one")]
        public void ThenTheItemsAreOrderedWithAQuatityOfOne()
        {
            ItemOrder = OrderPage.setItemQuatityToOne(ItemOrder);
        }
    }
}
