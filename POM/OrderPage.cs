using EccomerceAutomationTest.specs.StepsDefinition;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace EccomerceAutomationTest.specs.POM
{
    public class OrderPage : IClassFixture<WebDriverDefinition>
    {
        public WebDriverDefinition driverBase;
        public IWebDriver driver;

        private const string orderPageURL = "https://localhost:44315/order/my-orders";

        By getAllOrderListSelector = By.CssSelector("body > div > div > div");
        By detailOptionOfOrderSelector = By.ClassName("col-xs-1");
        By detailOptionOfOrderTagname = By.TagName("a");
        By getAllProductDisplayedSelector = By.CssSelector("body > div > div > div > section:nth-child(3)");
        By getProductNameSelector = By.ClassName("col-xs-3");
        By getProductQuantitySelector = By.ClassName("col-xs-1");
        public OrderPage(WebDriverDefinition driverBase)
        {
            driver = driverBase.getDriver();
        }
        public void navigateToOrders()
        {
            driver.Navigate().GoToUrl(orderPageURL);
        }
        public IList<IWebElement> listAllOrders()
        {
            var parentElement = driver.FindElement(getAllOrderListSelector);
            return parentElement.FindElements(By.TagName("article"));
        }
        public IList<IWebElement> listAllProductsInOrder()
        {
            var parentElement = driver.FindElement(getAllProductDisplayedSelector);
            return parentElement.FindElements(By.ClassName("esh-orders-detail-items--border"));
        }
        public void openLastOrder()
        {
            IList<IWebElement> elements = listAllOrders();
            var lastElement = elements.Last();
            var getDetailButton = lastElement.FindElements(detailOptionOfOrderSelector).First();
            getDetailButton.FindElement(detailOptionOfOrderTagname).Click();
        }
        public void checkOrderedProduct(Dictionary<string, string> itemOrdered)
        {
            navigateToOrders();
            openLastOrder();
            IList<IWebElement> articlesBought = listAllProductsInOrder();
            var countItem = 0;
            foreach (var item in articlesBought)
            {
                var itemName = item.FindElement(getProductNameSelector).Text.ToString();
                var itemQuantity = item.FindElements(getProductQuantitySelector).Last().Text.ToString();
                var element = itemOrdered.ElementAt(countItem);
                Assert.Equal(itemName.ToUpper(), element.Key);
                Assert.Equal(itemQuantity.ToUpper(), element.Value);
                countItem++;
            }
        }
        public Dictionary<string,
        string> setItemQuatityToOne(Dictionary<string, string> itemList)
        {
            Dictionary<string, string> newItemList = new Dictionary<string, string>();
            foreach (var item in itemList)
            {
                newItemList.Add(item.Key, "1");
            }
            return newItemList;
        }
    }
}