using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class RegjistrimiGjobes
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/admin/huazimet/kthe/8";

        [SetUp]
        public void Setup()
        {
            Config.Start(URL);
            Config.Login();
        }
        
        [Test]
        public void Regjistrimi_Gjobes_Me_Te_Dhena_Valide()
        {
            webDriver.FindElement(By.Id("shuma")).SendKeys("1");
            
            webDriver.FindElement(By.CssSelector("form > div > div:nth-child(12) > button")).Click();
            
            Assert.IsTrue(webDriver.PageSource.Contains("Libri u kthye me sukses!"));
        }

        [Test]
        public void Regjistrimi_Gjobes_Me_Te_Dhena_JoValide()
        {
            webDriver.FindElement(By.Id("shuma")).SendKeys("-1");

            webDriver.FindElement(By.CssSelector("form > div > div:nth-child(12) > button")).Click();
            
            Assert.IsTrue(webDriver.PageSource.Contains("Shuma duhet te jete me e madhe se 0"));
        }

        [TearDown]
        public void Close() => webDriver.Quit();
    }
}