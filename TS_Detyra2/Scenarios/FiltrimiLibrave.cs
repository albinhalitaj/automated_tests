using NUnit.Framework;
using OpenQA.Selenium;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class FiltrimiLibrave
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/libraria";

        [SetUp]
        public void Setup() => Config.Start(URL);

        [Test]
        public void Filtrimi_Librave_Me_Ane_Te_Nje_Kategorie()
        {
            webDriver.FindElement(By.LinkText("Science")).Click();
            var wrapper = webDriver.FindElement(By.Id("wrapper"));
            var librat = wrapper.FindElements(By.ClassName("category-item"));
            
            Assert.AreEqual(2,librat.Count);  
        }

        [Test]
        public void Filtrimi_Librave_Me_Te_Gjitha_Kategorite()
        {
            webDriver.FindElement(By.ClassName("btn-xs")).Click();
            var wrapper = webDriver.FindElement(By.Id("wrapper"));
            var librat = wrapper.FindElements(By.ClassName("category-item"));
            
            Assert.AreEqual(10,librat.Count); 
        }
        
        [TearDown]
        public void Close() => webDriver.Quit();
    }
}