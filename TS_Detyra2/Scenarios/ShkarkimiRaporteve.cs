using System;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class ShkarkimiRaporteve
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/admin";
        private static readonly string DownloadPath = Environment.GetEnvironmentVariable("USERPROFILE");

        [Test]
        public void Shkarkimi_Klienteve_Ne_Excel()
        {
            Config.Start($"{URL}/klienti");
            Config.Login();
            
            webDriver.FindElement(By.LinkText("Shkarko")).Click();

            Thread.Sleep(1000);
            
            var isDownloaded = CheckFileDownloaded("Klientët.xlsx");
            
            Assert.IsTrue(isDownloaded);
        }

        [Test]
        public void Shkarkimi_Librave_Ne_Excel()
        {
            Config.Start($"{URL}/libri");
            Config.Login();
            
            webDriver.FindElement(By.LinkText("Shkarko")).Click();
            
            Thread.Sleep(1000);

            var isDownloaded = CheckFileDownloaded("Librat.xlsx");
            
            Assert.IsTrue(isDownloaded);
        }

        [Test]
        public void Shkarkimi_Huazimeve_Ne_Excel()
        {
            Config.Start($"{URL}/huazimet");
            Config.Login();
            
            webDriver.FindElement(By.LinkText("Shkarko")).Click();
            
            Thread.Sleep(1000);

            var isDownloaded = CheckFileDownloaded("Huazimet.xlsx");
            
            Assert.IsTrue(isDownloaded);
        }

        [TearDown]
        public void Close()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                Config.TakeScreenshot(TestContext.CurrentContext.Test.Name);
            webDriver.Close();
        }

        private static bool CheckFileDownloaded(string filename)
        {
            var firstFile = Directory
                .GetFiles($"{DownloadPath}\\Downloads\\")
                .FirstOrDefault(fp => fp.Contains(filename));
            if (firstFile == default) return false;
            var fileInfo = new FileInfo(firstFile);
            var isFresh = DateTime.Now - fileInfo.LastWriteTime < TimeSpan.FromMinutes(3);
            File.Delete(firstFile);
            return isFresh;
        }
    }
}