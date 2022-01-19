using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class KthimiLibrave
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/admin/huazimet";

        [SetUp]
        public void Setup()
        {
            Config.Start(URL);
            Config.Login();
        }

        [Test]
        public void Kthimi_Nje_Libri_Te_Huazuar_Egzistues()
        {
            webDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/div[2]/div/div/div/div/div/div[2]/div/table/tbody/tr[3]/td[9]/div/a")).Click();
            
            webDriver.FindElement(By.XPath("form > div > div:nth-child(8) > button"));
            
            Assert.IsTrue(webDriver.PageSource.Contains("Libri u kthye me sukses!"));
        }

        [Test]
        public void Kthimi_Nje_Libri_Te_Huazuar_Pa_Specifikuar_Shumen_E_Gjobes()
        {
            webDriver.FindElement(By.CssSelector("#dataTable > tbody > tr:nth-child(1) > td.text-right > div > a.btn.btn-sm.bg-success-light.mr-2")).Click();
            
            webDriver.FindElement(By.CssSelector("form > div > div:nth-child(12) > button")).Click();
            
            Assert.IsTrue(webDriver.PageSource.Contains("Ju lutem vendosni nje shume per gjobe"));
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