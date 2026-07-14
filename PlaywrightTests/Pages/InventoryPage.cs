using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages;

public class InventoryPage
{
    private readonly IPage _page;
    public InventoryPage(IPage page) => _page = page;

    // локатор кнопки додавання рюкзака
    private ILocator AddBackpackButton => _page.Locator("#add-to-cart-sauce-labs-backpack");
    private ILocator CartIcon => _page.Locator(".shopping_cart_link");

    public async Task AddBackpackToCartAsync() => await AddBackpackButton.ClickAsync();
    public async Task GoToCartAsync() => await CartIcon.ClickAsync();
}