using ShoppingCart.EventFeed;

namespace ShoppingCart.ShoppingCart;

public class ShoppingCart {

    private HashSet<ShoppingCartItem> items = new();

    public int UserId { get; }

    public IEnumerable<ShoppingCartItem> Items => items;

    public ShoppingCart(int userId) => UserId = userId;

    public void AddItems(IEnumerable<ShoppingCartItem> shoppingCartItems, IEventStore eventStore) {
        
        foreach (var item in shoppingCartItems) {
            if (items.Add(item))
                eventStore.Raise("ShoppingCartItemAdded", new { UserId, item });
        }  
    }

    public void RemoveItems(int[] productCatalogueIds, IEventStore eventStore) {
        items.RemoveWhere(i => productCatalogueIds.Contains(i.ProductCatalogueId));
    }
}
