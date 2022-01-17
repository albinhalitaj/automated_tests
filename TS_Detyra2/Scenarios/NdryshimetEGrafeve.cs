using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TS_Detyra2.Configuration;

namespace TS_Detyra2.Scenarios
{
    public class NdryshimetEGrafeve
    {
        private readonly WebDriver webDriver = Config.SetupDriver();
        private const string URL = "https://localhost:5001/admin/klienti/shto";
        private const string BALLINA_URL = "https://localhost:5001/admin/ballina";

        [SetUp]
        public void Setup()
        {
            Config.Start(URL);
            Config.Login();
        }

        [Test]
        public void Ndryshimi_I_Hartes_Nga_Shtimi_Klientit()
        {
            ShtoKlientin();
            
            webDriver.Navigate().GoToUrl(BALLINA_URL);
            
            Thread.Sleep(1000);
            
            var chartSeriesGroup = webDriver.FindElement(By.ClassName("highcharts-series-group"));
            var chartSeries = chartSeriesGroup.FindElement(By.ClassName("highcharts-series"));

            var val = chartSeries.FindElement(By.ClassName("highcharts-name-prizren")).GetAttribute("fill");

            Assert.AreEqual("rgb(115,143,199)",val);
        }

        [Test]
        public void Ndryshimi_I_Bars_Nga_Huazimi_Ri()
        {
            var browserAction = new Actions(webDriver);
            
            var chartSeriesGroup = webDriver.FindElements(By.ClassName("highcharts-series-group"));
            var chartSeries = chartSeriesGroup[1].FindElement(By.ClassName("highcharts-series"));
            var highChartPoints = chartSeries.FindElements(By.ClassName("highcharts-point"));
            var rectTags = chartSeries.FindElement(By.TagName("rect"));
            browserAction.MoveToElement(rectTags).Perform();

        }

        [TearDown]
        public void Close() => webDriver.Quit();

        private void ShtoKlientin()
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
            qytetet.SelectByText("Prizren");
            webDriver.FindElement(By.Id("KodiPostal")).SendKeys(ClientData.KodiPostal);
            webDriver.FindElement(By.Id("NrKontaktues")).SendKeys(ClientData.NrKontaktues);
            webDriver.FindElement(By.Id("Emaili")).SendKeys(ClientData.Email);
            
            webDriver.FindElement(By.ClassName("btn-primary")).SendKeys(Keys.Enter);
        }
    }
}