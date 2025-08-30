using Core;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Pages;

public class FiltersPage : BasePage
{
    public FiltersPage(IWebDriver driver) : base(driver) { }

    private readonly By CreateFilterButton = By.XPath("//span[contains(text(), 'Add filter')]");
    private readonly By LaunchNameInput    = By.XPath("//input[@placeholder='Enter name']");
    private readonly By SaveButton         = By.XPath("//span[contains(text(), 'Save')]");
    private readonly By filtersMenuItem = By.XPath("//a[contains(@href,'/filters')]");
    private readonly By OnOffFilterDisplayingCheckbox = By.XPath("//input[@type='checkbox']/following-sibling::span[2]");
    private readonly By OnFilterDisplaying = By.XPath("//span[text()='ON']");
    private readonly By OffFilterDisplaying = By.XPath("//span[text()='OFF']");
    
    public void OpenFiltersSection()
    {
        FindClickable(filtersMenuItem);
        Click(filtersMenuItem);
    }

    public bool IsFilterPresent(string filterName)
    {
        return Find(By.XPath($"//span[text()='{filterName}']")).Displayed;
    }
    
    public bool IsDisplayOnLaunchesOn()
    {
        try
        {
            return FindVisible(OnFilterDisplaying).Displayed;
        }
        catch
        {
            return false;
        }
    }

    public void ToggleDisplayOnLaunches()
    {
        FindClickable(OnOffFilterDisplayingCheckbox);
        Click(OnOffFilterDisplayingCheckbox);
    }

    public void WaitForState(bool expectedOn)
    {
        if (expectedOn)
            FindVisible(OnFilterDisplaying);
        else
            FindVisible(OffFilterDisplaying);
    }
}