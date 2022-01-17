using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class RegjistrimiLibrit
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/admin/libri/shto";

        private const string BUTTON_SELECTOR =
            "#root > div > div.row > div > div > div > form > div > div:nth-child(13) > button";
        
        [SetUp]
        public void Setup()
        {
            Config.Start(URL);
            Config.Login();
        }
        
        [Test]
        public void Regjistrimi_Librit_Me_Te_Dhena_Valide()
        {
            webDriver.FindElement(By.Id(nameof(BookData.Titulli))).SendKeys(BookData.Titulli);
            webDriver.FindElement(By.Id(nameof(BookData.Autori))).SendKeys(BookData.Autori);
            webDriver.FindElement(By.Id(nameof(BookData.Botuesi))).SendKeys(BookData.Botuesi);
            var gjuha = new SelectElement(webDriver.FindElement(By.Id("GjuhaId")));
            var kategoria = new SelectElement(webDriver.FindElement(By.Id("KategoriaId")));
            var statusi = new SelectElement(webDriver.FindElement(By.Id("Statusi")));
            gjuha.SelectByText(BookData.Gjuha);
            kategoria.SelectByText(BookData.Kategoria);
            webDriver.FindElement(By.Id("Isbn")).SendKeys(BookData.ISBN);
            webDriver.FindElement(By.Id(nameof(BookData.Editioni))).SendKeys(BookData.Editioni);
            webDriver.FindElement(By.Id("NumriKopjeve")).SendKeys(BookData.Sasia);
            statusi.SelectByText(BookData.Statusi);
            webDriver.FindElement(By.Id("inputGroupFile04")).SendKeys(BookData.PhotoPath);
            webDriver.FindElement(
                By.CssSelector(BUTTON_SELECTOR)).Click();
            
            Thread.Sleep(1000);
            
            Assert.IsTrue(webDriver.PageSource.Contains(BookData.SuccessMessage));
        }

        [Test]
        public void Regjistrimi_Librit_Me_Te_Dhena_JoValide()
        {
            webDriver.FindElement(
                By.CssSelector(BUTTON_SELECTOR)).Click();
            
            Assert.IsTrue(webDriver.PageSource.Contains(BookData.ErrorMessage)); 
        }

        [TearDown]
        public void Close() => webDriver.Quit();
    }

    public static class BookData
    {
        public static string Titulli => "Automated Tests";
        public static string Autori => "Albin Halitaj";
        public static string Botuesi => "Dukagjini";
        public static string Gjuha => "Shqip";
        public static string Kategoria => "Science";
        public static string ISBN => "1234567890";
        public static string Editioni => "1";
        public static string Sasia => "5";
        public static string Statusi => "Aktiv";
        public static string PhotoPath => "C:\\Users\\albikk\\Desktop\\Products\\appleMac1.jpg";
        public const string SuccessMessage = "Libri u shtua!";
        public const string ErrorMessage = "Kjo fushë është e obligueshme";
    }
}