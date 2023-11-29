using ShoppingCart.ShoppingCart;

namespace ShoppingCart.ProductCatalogue;

public class ProductCatalogueClient : IProductCatalogueClient {
    public Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogueIds) {
        throw new NotImplementedException();
    }
}
