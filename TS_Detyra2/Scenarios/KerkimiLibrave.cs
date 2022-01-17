using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class KerkimiLibrave
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001";

        [SetUp]
        public void Setup() => Config.Start(URL);

        [Test]
        public void Kerkimi_Librave_Me_Ane_Te_Autorit()
        {
            Wait(3);
            
            webDriver.FindElement(By.LinkText("Autori")).Click();
            webDriver.FindElement(By.CssSelector("#author > form > div > input")).SendKeys("ismail");
            webDriver.FindElement(By.CssSelector("#author > form > div > input")).SendKeys(Keys.Enter);

            var wrapper = webDriver.FindElement(By.Id("wrapper"));
            var books = wrapper.FindElements(By.ClassName("category-item"));

            foreach (var book in books)
            {
                Assert.IsTrue(book.Text.ToLower().Contains("ismail"));
            }
        }

        [Test]
        public void Kerkimi_Librave_Me_Ane_Te_Titullit()
        {
            Wait(3);
            webDriver.FindElement(By.CssSelector("#book > form > div > input")).SendKeys("programimi");
            webDriver.FindElement(By.CssSelector("#book > form > div > input")).SendKeys(Keys.Enter);
            

            var wrapper = webDriver.FindElement(By.Id("wrapper"));
            var books = wrapper.FindElements(By.ClassName("category-item"));

            foreach (var book in books)
            {
                Assert.IsTrue(book.Text.ToLower().Contains("programimi"));
            }
        }

        [Test]
        public void Kerkimi_Librave_Me_Ane_Te_Botuesit()
        {
            
        }

        private void Wait(int seconds = 0) => webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);

        [TearDown]
        public void Close() => webDriver.Close();
    }
}