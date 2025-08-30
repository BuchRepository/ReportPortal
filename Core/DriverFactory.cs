using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace Core;

public static class DriverFactory
{
    public static IWebDriver CreateDriver(string browser = "chrome")
    {
        IWebDriver driver;

        switch (browser.ToLower())
        {
            case "firefox":
                driver = new FirefoxDriver();
                break;
            case "safari":
                driver = new SafariDriver();
                break;
            case "chrome":
                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.EnableVerboseLogging = true;
                
                var options = new ChromeOptions();
                options.AddArgument("--headless");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920,1080"); 
                
                driver = new ChromeDriver(service, options);
                break;
            
            default:
                throw new NotSupportedException($"Browser '{browser}' is not supported.");
        }

        return driver;
    }
}