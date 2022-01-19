using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class NdryshimiFjalekalimit
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/admin/profili";

        [SetUp]
        public void Setup()
        {
            Config.Start(URL);
            Config.Login();
        }

        [Test]
        public void Ndryshimi_Fjalekalimit_Me_Fjalekalim_Te_Sakte()
        {
           webDriver.FindElement(By.LinkText("Passwordi")).Click();
           Thread.Sleep(1000);
           webDriver.FindElement(By.Id("old")).SendKeys(UserInfo.OldPassword);
           webDriver.FindElement(By.Id("newPass")).SendKeys(UserInfo.NewPassword);
           webDriver.FindElement(By.Id("confirmNew")).SendKeys(UserInfo.ConfirmedNewPassword);
           webDriver.FindElement(By.CssSelector("#password_tab > div > div > div > div > form > button")).Click();
           
           Thread.Sleep(2000);
           
           Assert.IsTrue(webDriver.PageSource.Contains("Fjalëkalimi u përditësua!"));
        }

        [Test]
        public void Ndryshimi_Fjalekalimit_Me_Fjalekalim_Te_Pasakte()
        {
            webDriver.FindElement(By.LinkText("Passwordi")).Click();
            Thread.Sleep(1000);
            webDriver.FindElement(By.Id("old")).SendKeys(UserInfo.WrongOldPassword);
            webDriver.FindElement(By.Id("newPass")).SendKeys(UserInfo.NewPassword);
            webDriver.FindElement(By.Id("confirmNew")).SendKeys(UserInfo.ConfirmedNewPassword); 
            webDriver.FindElement(By.CssSelector("#password_tab > div > div > div > div > form > button")).Click();

            Assert.IsTrue(webDriver.PageSource.Contains("Fjalëkalimi i vjetër është gabim!"));
        }

        [TearDown]
        public void Close()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                Config.TakeScreenshot(TestContext.CurrentContext.Test.Name);
            webDriver.Quit();
        }
    }

    public static class UserInfo
    {
        public static string OldPassword => "halitaj";
        public static string WrongOldPassword => "halitaj1";
        public static string NewPassword => "albinhalitaj";
        public static string ConfirmedNewPassword => "albinhalitaj";
    }
}