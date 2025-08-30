using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using Pages;

namespace Core;

public class TestBase
{
    protected IWebDriver? Driver;
    
    protected LoginPage? LoginPage;
    protected FiltersPage? FiltersPage;
    
    protected static ExtentReports extent;
    protected ExtentTest test; 
    
    [OneTimeSetUp]
    public void SetupReporter()
    {
        string reportPath = Path.Combine(AppContext.BaseDirectory, "ExtentReports.html");
        TestContext.Progress.WriteLine($"[LOG] ExtentReports path: {reportPath}");
        
        var htmlReporter = new ExtentHtmlReporter(reportPath);
        htmlReporter.Config.DocumentTitle = "Test Report";
        htmlReporter.Config.ReportName = "UI Test Report";
        htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
        extent = new ExtentReports();
        extent.AttachReporter(htmlReporter);
    }
        
    [SetUp]
    public void SetUp()
    {
        test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        
        string browser = TestContext.Parameters.Get("browser", "chrome");
        Driver = DriverFactory.CreateDriver(browser);
        Driver.Navigate().GoToUrl("https://rp.epam.com");
        
        LoginPage = new LoginPage(Driver);
        FiltersPage = new FiltersPage(Driver);
    }

    [TearDown]
    public void TearDown()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        var message = TestContext.CurrentContext.Result.Message;
        
        switch (status)
        {
            case NUnit.Framework.Interfaces.TestStatus.Passed:
                test.Pass("Test passed");
                break;
            case NUnit.Framework.Interfaces.TestStatus.Failed:
                test.Fail(message);
                break;
            default:
                test.Skip("Test skipped");
                break;
        }
        
        Driver.Quit();
        Driver.Dispose();
        
        Driver = null;
        LoginPage = null;
        FiltersPage = null;

        extent.Flush();
    }
}