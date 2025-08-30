using OpenQA.Selenium;
using Pages;

namespace Core;

public class TestBase
{
    protected IWebDriver? Driver;
    
    protected LoginPage? LoginPage;
    protected FiltersPage? FiltersPage;
        
    [SetUp]
    public void SetUp()
    {
        string browser = TestContext.Parameters.Get("browser", "chrome");
        Driver = DriverFactory.CreateDriver(browser);
        Driver.Navigate().GoToUrl("https://rp.epam.com");
        
        LoginPage = new LoginPage(Driver);
        FiltersPage = new FiltersPage(Driver);
    }

    [TearDown]
    public void TearDown()
    {
        Driver.Quit();
        Driver.Dispose();
        
        Driver = null;
        LoginPage = null;
        FiltersPage = null;
    }
}