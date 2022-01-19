using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class Autorizimi
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/admin/stafi";

        [SetUp]
        public void Setup() => Config.Start(URL);

        [Test]
        public void Autorizimi_Punetorit()
        {
            Login(UserType.Punetor);
            Assert.IsTrue(webDriver.PageSource.Contains("Oops! Nuk jeni i autorizuar!"));
        }

        [Test]
        public void Autorizimi_Administratorit()
        {
            Login(UserType.Admin);
            var wrapper = webDriver.FindElement(By.Id("wrapper"));
            var users = wrapper.FindElements(By.ClassName("card"));
            Assert.AreEqual(3,users.Count);
        }

        private void Login(UserType user)
        {
            switch (user)
            {
                case UserType.Admin:
                    webDriver.FindElement(By.Id("Username")).SendKeys(AuthInfo.AdminUsername);
                    webDriver.FindElement(By.Id("Password")).SendKeys(AuthInfo.AdminPassword);
                    break;
                case UserType.Punetor:
                    webDriver.FindElement(By.Id("Username")).SendKeys(AuthInfo.WorkerUsername);
                    webDriver.FindElement(By.Id("Password")).SendKeys(AuthInfo.WorkerPassword);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(user), user, null);
            }
            webDriver.FindElement(By.ClassName("btn-primary")).SendKeys(Keys.Enter);
        }

        [TearDown]
        public void Close()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                Config.TakeScreenshot(TestContext.CurrentContext.Test.Name);
            }
            webDriver.Quit();
        }
    }

    public enum UserType
    {
        Admin = 0,
        Punetor = 1
    }
    
    public static class AuthInfo
    {
        public static string WorkerUsername => "endrit";
        public static string WorkerPassword => "hyseni";
        public static string AdminUsername => "albin";
        public static string AdminPassword => "halitaj";
    }
}