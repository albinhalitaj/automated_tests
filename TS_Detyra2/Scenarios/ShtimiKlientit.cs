using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class ShtimiKlientit
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/admin/klienti/shto";
        
        [SetUp]
        public void Setup()
        {
            Config.Start(URL);
            Config.Login();
        }

        [Test]
        public void Regjistrimi_Klientit_Me_Te_Dhena_Valide()
        {
            webDriver.FindElement(By.Id("Emri")).SendKeys(ClientData.Emri);
            webDriver.FindElement(By.Id("Mbiemri")).SendKeys(ClientData.Mbiemri);
            webDriver.FindElement(By.Id("Datalindjes")).SendKeys(ClientData.Datalindjes);
            var gjinite = new SelectElement(webDriver.FindElement(By.Id("Gjinia")));
            gjinite.SelectByText(ClientData.Gjinia);
            webDriver.FindElement(By.Id("NrPersonal")).SendKeys(ClientData.NrPersonal);
            webDriver.FindElement(By.Id("Adresa")).SendKeys(ClientData.Adresa);
            var shtetet = new SelectElement(webDriver.FindElement(By.Id("countrylist")));
            shtetet.SelectByValue(ClientData.Shteti);
            var qytetet = new SelectElement(webDriver.FindElement(By.Id("citiesList")));
            qytetet.SelectByText(ClientData.Qyteti);
            webDriver.FindElement(By.Id("KodiPostal")).SendKeys(ClientData.KodiPostal);
            webDriver.FindElement(By.Id("NrKontaktues")).SendKeys(ClientData.NrKontaktues);
            webDriver.FindElement(By.Id("Emaili")).SendKeys(ClientData.Email);
            
            webDriver.FindElement(By.ClassName("btn-primary")).SendKeys(Keys.Enter);
            
            Thread.Sleep(1000);
            
            Assert.IsTrue(webDriver.PageSource.Contains(ClientData.SuccessMessage));
        }

        [Test]
        public void Regjistrimi_Klientit_Me_Te_Dhena_JoValide()
        {
            webDriver.FindElement(By.ClassName("btn-primary")).SendKeys(Keys.Enter);
            
            Assert.IsTrue(webDriver.PageSource.Contains(ClientData.ErrorMessage));
        }

        [TearDown]
        public void Close()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                Config.TakeScreenshot(TestContext.CurrentContext.Test.Name);
            webDriver.Quit();
        }
    }

    public static class ClientData
    {
        public static string Emri => "Albin";
        public static string Mbiemri => "Halitaj";
        public static string Gjinia => "Mashkull";
        public static string Datalindjes => "01/12/2022";
        public static string NrPersonal => "1234567890122345";
        public static string Adresa => "Sopije";
        public static string Shteti => "Kosovë";
        public static string Qyteti => "Prishtinë";
        public static string KodiPostal => "12345";
        public static string Email => "testim.testeri@gmail.com";
        public static string NrKontaktues => "049538128";
        public static string SuccessMessage => "Klienti u shtua!";
        public static string ErrorMessage => "Kjo fushë është e obligueshme";
    }
}