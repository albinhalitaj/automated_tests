using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TS_Detyra2.Configuration
{
    public static class Config
    {
        private static readonly WebDriver _webDriver = new ChromeDriver();
        private const string USERNAME = "albin";
        private const string PASSWORD = "halitaj";
        private const string ScreenshotPath = "C:\\Users\\albikk\\Desktop\\OpenSource\\TS_Detyra2\\Screenshots";

        public static void Start(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
            _webDriver.Manage().Window.Maximize();
        }

        public static void Login()
        {
            _webDriver.FindElement(By.Id("Username")).SendKeys(USERNAME);
            _webDriver.FindElement(By.Id("Password")).SendKeys(PASSWORD);

            _webDriver.FindElement(By.ClassName("btn-primary")).SendKeys(Keys.Enter);
        }

        public static void TakeScreenshot(string fileName)
        {
            var screenshot = ((ITakesScreenshot)_webDriver).GetScreenshot();
            screenshot.SaveAsFile($"{ScreenshotPath}/{fileName}.png",ScreenshotImageFormat.Png);
            
        }

        public static WebDriver SetupDriver() => _webDriver;
    }
}