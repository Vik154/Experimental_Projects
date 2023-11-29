using Nancy;
using Nancy.ModelBinding;
using ShoppingCart.EventFeed;
using ShoppingCart.ProductCatalogue;

namespace ShoppingCart.ShoppingCart;

/// <summary> Конечная точка для доступа к корзине заказов по ID пользователя </summary>
public class ShoppingCartModule : NancyModule {

    /// <summary> Все маршруты в этом модуле начинаются с /shoppingcart </summary>
    public ShoppingCartModule(
        IShoppingCartStore shoppingCartStore,
        IProductCatalogueClient productCatalogue,
        IEventStore eventStore
        )
        : base("/shoppingcart")
    {
        /// <summary> 
        /// Объявление конечной точки для обработки запросов к URL /shoppingcart/{userid}
        /// например shoppingcart/123
        /// 
        /// лямбда-выражение: parameters => {......}
        /// Это обработчик маршрута, представляющий собой фрагмент кода, выполняемый всякий 
        /// раз при получении микросервисом Shopping Cart запроса к URL, соответствующему 
        /// объявлению маршрута.Например, когда шлюз API запрашивает корзину заказов через
        /// URL shoppingcart / 123, именно этот код обрабатывает запрос.
        /// </summary>
        Get("/{userid:int}", parameters => {
            var userId = (int)parameters.userid;
            return shoppingCartStore.Get(userId);
        });


        /// <summary> Добавление товаров в корзину заказов </summary>
        Post("/{userid:int}/items", async (parameters, _) => {

            // Чтение и десериализация массива Id товаров из тела HTTP
            var productCatalogueIds = this.Bind<int[]>();
            var userId = (int)parameters.userid;

            var shoppingCart = shoppingCartStore.Get(userId);

            // Извлечение информации о товарах из микросервиса ProductCatalog
            var shoppingCartItems = await productCatalogue
                .GetShoppingCartItems(productCatalogueIds)
                .ConfigureAwait(false);

            shoppingCart.AddItems(shoppingCartItems, eventStore);   // Добавить товар в корзину
            shoppingCartStore.Save(shoppingCart);                   // Сохранить обновленную корзину в хранилище

            return shoppingCart;    // Вернуть обновленную корзину товаров
        });

        /// <summary> Конечная точка для удаления товаров из корзины заказов </summary>
        Delete("/{userid:int}/items", parameters => {
            var productCatalogueIds = this.Bind<int[]>();
            var userId = (int)parameters.userid;

            var shoppingCart = shoppingCartStore.Get(userId);
            shoppingCart.RemoveItems(productCatalogueIds, eventStore);
            shoppingCartStore.Save(shoppingCart);

            return shoppingCart;
        });
    }

}
