using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class FshirjaKlienteve
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/admin/klienti";

        [Test]
        public void Fshirja_E_Nje_Klienti_Egzistues()
        {
            Config.Start(URL);
            Config.Login();
            
            webDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/div[2]/div/div/div/div/div/div[2]/div/table/tbody/tr[7]/td[10]/div/a[2]")).Click();
            
            webDriver.FindElement(By.XPath("/html/body/div[5]/div/div[3]/button[1]")).Click();
            
            webDriver.Navigate().GoToUrl(URL);
            
            Thread.Sleep(1000);

            var table = webDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/div[2]/div/div/div/div/div/div[2]/div/table/tbody"));

            var rows = table.FindElements(By.TagName("tr"));
            
            Assert.AreEqual(6,rows.Count);
        }

        [Test]
        public void Fshirja_E_Nje_Klienti_Joegzistues()
        {
            const string url = "https://localhost:5001/admin/klienti/delete/100";
            
            Config.Start(url);
            Config.Login();

            var currentUrl = webDriver.Url;
            
            Assert.AreEqual(URL,currentUrl);
        }

        [TearDown]
        public void Close()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                Config.TakeScreenshot(TestContext.CurrentContext.Test.Name);
            }
            webDriver.Close();
        }
    }
}