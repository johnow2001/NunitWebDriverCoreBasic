using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;

namespace NUnitTestProject2
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl("https://www.google.com/");
            IWebElement element = driver.FindElement(By.Name("q"));
            element.SendKeys("Selenium webdriver");
            element.Submit();    
        }

        [Test]
        public void GotoBBCHomePage()
        {
            driver.Navigate().GoToUrl("https://www.bbc.co.uk");
            string h2 = driver.FindElement(By.ClassName("hp-banner__text")).Text;
            Console.WriteLine("h2 = " + h2);
            Assert.That("Welcome to the BBC", Is.EqualTo(h2));
            driver.FindElement(By.LinkText("Sport")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement rugby = wait.Until(d => driver.FindElement(By.LinkText("Rugby U")));
            rugby.Click();
            System.Threading.Thread.Sleep(1000);
        }

        [TestCase ("https://www.google.com/", ExpectedResult = "https://www.google.com/")]
        [TestCase("https://www.bbc.co.uk/", ExpectedResult = "https://www.bbc.co.uk/")]
        public string VisitDifferentSites(string url)
        {
            driver.Navigate().GoToUrl(url);
            string visitedUrl = driver.Url;
            Assert.AreEqual(url, visitedUrl);
            return visitedUrl;
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}