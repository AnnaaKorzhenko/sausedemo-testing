using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightTests.Pages;
using System.Threading.Tasks;

namespace PlaywrightTests.Tests;

public class SauceDemoTests : PageTest
{
    private string _baseUrl = null!;
    private string _password = null!;

    [SetUp]
    public void Setup()
    {
        _baseUrl = TestContext.Parameters["BaseUrl"] ?? "https://www.saucedemo.com/";
        _password = TestContext.Parameters["GlobalPassword"] ?? "secret_sauce";
    }


    [TestCase("locked_out_user", "Epic sadface: Sorry, this user has been locked out.")]
    public async Task Login_InvalidOrLockedUser_ShowsErrorMessage(string username, string expectedError)
    {
        // Arrange
        var loginPage = new LoginPage(Page);

        // Act
        await loginPage.GotoAsync(_baseUrl);
        await loginPage.LoginAsync(username, _password);

        // Assert
        await Expect(loginPage.ErrorMessage).ToContainTextAsync(expectedError);
    }

    [Test]
    public async Task Checkout_ValidStandardUser_CompletesOrderSuccessfully()
    {
        // Arrange
        var validUser = TestContext.Parameters["ValidUser"] ?? "standard_user";
        
        var loginPage = new LoginPage(Page);
        var inventoryPage = new InventoryPage(Page);
        
        var firstName = "Anna";
        var lastName = "K";
        var zipCode = "01001";

        // Act
        // логін
        await loginPage.GotoAsync(_baseUrl);
        await loginPage.LoginAsync(validUser, _password);

        // кошик
        await inventoryPage.AddBackpackToCartAsync();
        await inventoryPage.GoToCartAsync();

        // замовлення
        await Page.Locator("#checkout").ClickAsync();
        await Page.Locator("#first-name").FillAsync(firstName);
        await Page.Locator("#last-name").FillAsync(lastName);
        await Page.Locator("#postal-code").FillAsync(zipCode);
        await Page.Locator("#continue").ClickAsync();
        await Page.Locator("#finish").ClickAsync();

        // Assert
        var successMessage = Page.Locator(".complete-header");
        await Expect(successMessage).ToHaveTextAsync("Thank you for your order!");
    }
}