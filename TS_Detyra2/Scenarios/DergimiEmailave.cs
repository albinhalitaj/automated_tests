using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class DergimiEmailave
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/admin/huazimet/njofto/8";
        private const string BUTTON_SELECTOR = "form > div.form-group.mb-0 > div > button.btn.btn-primary";

        [SetUp]
        public void Setup()
        {
            Config.Start(URL);
            Config.Login();
        }
        
        [Test]
        public void Dergimi_Emailit_Pa_Subjekt_Valid()
        {
           webDriver.FindElement(By.ClassName("note-editable")).SendKeys(EmailData.Mesazhi); 
           
           webDriver.FindElement(By.CssSelector(BUTTON_SELECTOR)).Click();

           var invalid = webDriver.FindElement(By.Id("Mesazhi_Subjekti")).GetAttribute("class");
           
           Assert.AreEqual(EmailData.Invalid,invalid);
           
        }

        [Test]
        public void Dergimi_Emailit_Pa_Mesazh_Valid()
        {
            webDriver.FindElement(By.Id("Mesazhi_Subjekti")).SendKeys(EmailData.Subjekti);
            
            webDriver.FindElement(By.CssSelector(BUTTON_SELECTOR)).Click();
            
            Assert.IsTrue(webDriver.PageSource.Contains(EmailData.ErrorMessage));
        }

        [TearDown]
        public void Close()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                Config.TakeScreenshot(TestContext.CurrentContext.Test.Name);
            webDriver.Quit();
        }
    }

    public static class EmailData
    {
        public static string Mesazhi => "Test Mesazhi";
        public static string Subjekti => "Test Subjekti";
        public static string Invalid => "form-control input-validation-error";
        public static string ErrorMessage => "Kjo fushë është e obligueshme";
    }
}