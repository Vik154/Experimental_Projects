using Nancy;
using Nancy.ModelBinding;

namespace ShoppingCart.ShoppingCart;

/// <summary> Конечная точка для доступа к корзине заказов по ID пользователя </summary>
public class ShoppingCartModule : NancyModule {

    /// <summary> Все маршруты в этом модуле начинаются с /shoppingcart </summary>
    public ShoppingCartModule(IShoppingCartStore shoppingCartStore)
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
    }

}
