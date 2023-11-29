using ShoppingCart.ShoppingCart;

namespace ShoppingCart.ProductCatalogue;

public interface IProductCatalogueClient {
    Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogueIds);
}
