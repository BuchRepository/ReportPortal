using Core;
using OpenQA.Selenium;

namespace Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }	

        private readonly By LoginInput = By.XPath("//*[@name='login']");
        private readonly By PasswordInput = By.XPath("//*[@name='password']");
        private readonly By SubmitButton = By.XPath("//*[@type='submit']");

        public void Login(string login, string password)
        {
            Type(LoginInput, login);
            Type(PasswordInput, password);
            Click(SubmitButton);
        }
    }
}