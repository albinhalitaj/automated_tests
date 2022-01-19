using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class HuazimiLibrave
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/admin/huazimet/huazo";

        [SetUp]
        public void Setup()
        {
            Config.Start(URL);
            Config.Login();
        }

        [Test]
        public void Huazimi_Librave_Me_Te_Dhena_Valide()
        {
            new SelectElement(webDriver.FindElement(By.Id("KlientiId"))).SelectByText("Endrit Hyseni");
            new SelectElement(webDriver.FindElement(By.Id("LibriId"))).SelectByText("Keshtjella");
            webDriver.FindElement(By.Id("NumriKopjeve")).SendKeys("1");
            webDriver.FindElement(By.Id("DataHuazimi")).SendKeys("01/15/2022");
            webDriver.FindElement(By.Id("DataKthimit")).SendKeys("01/30/2022");
            webDriver.FindElement(By.Id("Pershkrimi")).SendKeys("test");
            webDriver.FindElement(By.CssSelector("form > div > div:nth-child(8) > button")).Click();
            
            Assert.IsTrue(webDriver.PageSource.Contains("Libri u huazua me sukses!"));
        }

        [Test]
        public void Huazimi_Librave_Me_Te_Dhena_JoValide()
        {
            webDriver.FindElement(By.CssSelector("form > div > div:nth-child(8) > button")).Click();
            
            Assert.IsTrue(webDriver.PageSource.Contains("Kjo fushë është e obligueshme"));
        }

        [Test]
        public void Huaizimi_Librit_Me_Sasi_Me_Te_Madhe_Se_Ne_Stok()
        {
            new SelectElement(webDriver.FindElement(By.Id("KlientiId"))).SelectByText("Endrit Hyseni");
            new SelectElement(webDriver.FindElement(By.Id("LibriId"))).SelectByText("Keshtjella");
            webDriver.FindElement(By.Id("NumriKopjeve")).SendKeys("100");
            webDriver.FindElement(By.Id("DataHuazimi")).SendKeys("01/15/2022");
            webDriver.FindElement(By.Id("DataKthimit")).SendKeys("01/30/2022");
            webDriver.FindElement(By.Id("Pershkrimi")).SendKeys("test"); 
            
            Assert.IsTrue(webDriver.PageSource.Contains("Numri i kopjeve nuk mund te jete me i madhe se sasia ne stok"));
        }

        [TearDown]
        public void Close()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                Config.TakeScreenshot(TestContext.CurrentContext.Test.Name);
            webDriver.Quit();
        }
    }
}