using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class EditimiKlientit
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/admin/klienti/edito/id0002";

        [SetUp]
        public void Setup()
        {
            Config.Start(URL);
            Config.Login();
        }

        [Test]
        public void Editimi_Klientit_Duke_Lere_Fushat_E_Zbrazta()
        {
            ClearControls();
            
            webDriver.FindElement(By.ClassName("btn-primary")).SendKeys(Keys.Enter);
            
            Assert.IsTrue(webDriver.PageSource.Contains("Kjo fushë është e obligueshme"));
        }

        [Test]
        public void Editimi_Klientit_Me_Te_Dhena_Valide()
        {
            ClearControls();
            
            webDriver.FindElement(By.Id("Emri")).SendKeys("Testim");
            webDriver.FindElement(By.Id("Mbiemri")).SendKeys("Testeri");
            webDriver.FindElement(By.Id("Datalindjes")).SendKeys("12/12/2001");
            var gjinite = new SelectElement(webDriver.FindElement(By.Id("Gjinia")));
            gjinite.SelectByIndex(0);
            webDriver.FindElement(By.Id("NrPersonal")).SendKeys("12345678901");
            webDriver.FindElement(By.Id("Adresa")).SendKeys("Fadil Elshani");
            webDriver.FindElement(By.Id("Shteti")).SendKeys("Kosovë");
            webDriver.FindElement(By.Id("Qyteti")).SendKeys("Prishtinë");
            webDriver.FindElement(By.Id("KodiPostal")).SendKeys("23000");
            webDriver.FindElement(By.Id("NrKontaktues")).SendKeys("049200236");
            webDriver.FindElement(By.Id("Emaili")).SendKeys("albinhalitaj@gmail.com");
            
            webDriver.FindElement(By.ClassName("btn-primary")).SendKeys(Keys.Enter);
            
            Assert.IsTrue(webDriver.PageSource.Contains("Klienti u përditësua!")); 
        }

        private void ClearControls()
        {
            webDriver.FindElement(By.Id("Emri")).Clear();
            webDriver.FindElement(By.Id("Mbiemri")).Clear();
            webDriver.FindElement(By.Id("Datalindjes")).Clear();
            var gjinite = new SelectElement(webDriver.FindElement(By.Id("Gjinia")));
            gjinite.SelectByIndex(0);
            webDriver.FindElement(By.Id("NrPersonal")).Clear();
            webDriver.FindElement(By.Id("Adresa")).Clear();
            webDriver.FindElement(By.Id("Shteti")).Clear();
            webDriver.FindElement(By.Id("Qyteti")).Clear();
            webDriver.FindElement(By.Id("KodiPostal")).Clear();
            webDriver.FindElement(By.Id("NrKontaktues")).Clear();
            webDriver.FindElement(By.Id("Emaili")).Clear(); 
        } 

        [TearDown]
        public void Close() => webDriver.Quit();

    }
}