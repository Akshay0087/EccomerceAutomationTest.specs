using EccomerceAutomationTest.specs.StepsDefinition;
using NLog;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Xunit;
namespace EccomerceAutomationTest.specs.POM
{
    public class BasketPage : IClassFixture<WebDriverDefinition>
    {
        public WebDriverDefinition driverBase;
        public IWebDriver driver;
        public
        const string homePageURL = "https://localhost:44315/";
        public
        const string orderPageURL = "https://localhost:44315/order/my-orders";
        public Dictionary<string,string> itemOrdered;
        

       

        By getAllProductOnCurrentPageSelector = By.XPath("/html/body/div/div/div[2]");
        By nextButtonID = By.Id("Next");
        By checkoutButtonSelector = By.CssSelector("body > div > div > form > div > div.row > section.esh-basket-item.col-xs-push-7.col-xs-4 > a");
        By payButtonSelector = By.CssSelector("body > div > div > form > div > div.row > section.esh-basket-item.col-xs-push-7.col-xs-4.text-right > input");
        By confirmationMessageSelector = By.CssSelector("body > div > div > h1");
        By productFormElementTagname = By.TagName("form");
        By productDetailClass = By.ClassName("esh-catalog-name");
        By productNameTag = By.TagName("span");
        By productClickClass = By.ClassName("esh-catalog-button");
        By basketUpdateButonName = By.Name("updatebutton");
        public BasketPage(WebDriverDefinition driverBase)
        {
            driver = driverBase.getDriver();
            //this.driverBase = driverBase;
        }
        public void clickOnCheckout()
        {
            driver.FindElement(checkoutButtonSelector).Click();
        }
        public string GetPageTitle()
        {
            return driver.Title;
        }
        public void clickOnPay()
        {
            driver.FindElement(payButtonSelector).Click();
        }
        public string GetConfirmationMessage()
        {
            return driver.FindElement(confirmationMessageSelector).Text;
        }
        public static Dictionary<string,
        string> ToDictionary(Table table)
        {
            var dictionary = new Dictionary<string,
                string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            return dictionary;
        }
        public void navigateToHomepage()
        {
            driver.Navigate().GoToUrl(homePageURL);
        }
        public IList<IWebElement> getAllProductOnPage()
        {
            try
            {
                var parentElement = driver.FindElement(getAllProductOnCurrentPageSelector);
                return parentElement.FindElements(By.ClassName("col-md-4"));
            }
            catch (Exception ex)
            {
             
                throw ex;

            }
        }
        public string getElementName(IWebElement element)
        {
            try
            {
                var formElement = element.FindElement(productFormElementTagname);
                var divElement = formElement.FindElement(productDetailClass);
                return divElement.FindElement(productNameTag).Text;
                
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
        public void selectCurrentElement(IWebElement element)
        {
            var formElement = element.FindElement(productFormElementTagname);
            formElement.FindElement(productClickClass).Click();
        }
        public void setitemQuantity(int counter, string value)
        {
            driver.FindElement(By.Name("Items[" + counter + "].Quantity")).Clear();
            driver.FindElement(By.Name("Items[" + counter + "].Quantity")).SendKeys(value);
        }
        public void updateBasket()
        {
            driver.FindElement(basketUpdateButonName).Click();
            navigateToHomepage();
        }
        public void selectUserItem(Table table)
        {
            var dictionaryData = ToDictionary(table);
            int count = 0;
            foreach (var row in dictionaryData)
            {
                var isItemFound = false;
                while (!isItemFound)
                {
                    IList<IWebElement> elements = getAllProductOnPage();
                    if (elements == null)
                    {
                        break;
                    }
                    foreach (IWebElement element in elements)
                    {
                        if (getElementName(element) == row.Key)
                        {
                            selectCurrentElement(element);
                            isItemFound = true;
                            Assert.Equal("Basket - Microsoft.eShopOnWeb", GetPageTitle());
                            setitemQuantity(count, row.Value);
                            updateBasket();
                            count++;
                            break;
                        }
                    }
                    if (!isItemFound && driver.FindElements(nextButtonID).Count > 0)
                    {
                        driver.FindElement(nextButtonID).Click();
                    }
                    if (!isItemFound && driver.FindElements(nextButtonID).Count == 0)
                    {
                        Assert.Fail("Item not found. Item: " + row.Key);
                        break;
                    }
                }
                if (!isItemFound)
                {
                    Assert.Fail("Product " + row.Key + " does not exist");
                }
            }
        }
    }
}