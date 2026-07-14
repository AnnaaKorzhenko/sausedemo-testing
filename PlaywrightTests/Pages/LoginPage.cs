using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages;

public class LoginPage
{
    private readonly IPage _page;
    public LoginPage(IPage page) => _page = page;

    private ILocator UsernameInput => _page.Locator("#user-name");
    private ILocator PasswordInput => _page.Locator("#password");
    private ILocator LoginButton => _page.Locator("#login-button");
    public ILocator ErrorMessage => _page.Locator("[data-test='error']");

    public async Task GotoAsync(string baseUrl) 
    {
        await _page.GotoAsync(baseUrl);
    }

    public async Task LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
    }
}