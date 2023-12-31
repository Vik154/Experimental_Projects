﻿using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using ShoppingCart.ShoppingCart;

namespace ShoppingCart.ProductCatalogue;

public class ProductCatalogueClient : IProductCatalogueClient {

    // Стратегия обработки ошибок микросервиса
    private static AsyncRetryPolicy exponentialRetryPolicy = 
        Policy.Handle<Exception>().WaitAndRetryAsync(3, attempt => 
            TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)), (ex, _) => Console.WriteLine(ex.ToString())
            );

    // HTTP-запрос типа GET к микросервису Product Catalog
    private static string productCatalogueBaseUrl = 
        @"http://private-05cc8-chapter2productcataloguemicroservice.apiary-mock.com";
    private static string getProductPathTemplate =
      "/products?productIds=[{0}]";

    public Task<IEnumerable<ShoppingCartItem>>
      GetShoppingCartItems(int[] productCatalogueIds) => exponentialRetryPolicy
        .ExecuteAsync(async () => await GetItemsFromCatalogueService(productCatalogueIds).ConfigureAwait(false));

    // Извлечение товаров и преобразование их в элементы корзины заказов
    private async Task<IEnumerable<ShoppingCartItem>> GetItemsFromCatalogueService(int[] productCatalogueIds) {
        var response = await RequestProductFromProductCatalogue(productCatalogueIds).ConfigureAwait(false);
        return await ConvertToShoppingCartItems(response).ConfigureAwait(false);
    }

    // HTTP-запрос типа GET к микросервису Product Catalog
    private static async Task<HttpResponseMessage> RequestProductFromProductCatalogue(int[] productCatalogueIds) {
        var productsResource = string.Format(
          getProductPathTemplate, string.Join(",", productCatalogueIds));
        using (var httpClient = new HttpClient()) {
            httpClient.BaseAddress = new Uri(productCatalogueBaseUrl);
            return await httpClient.GetAsync(productsResource).ConfigureAwait(false);
        }
    }

    // Извлечение данных из ответа
    private static async Task<IEnumerable<ShoppingCartItem>> ConvertToShoppingCartItems(HttpResponseMessage response) {
        
        response.EnsureSuccessStatusCode();

        var products = JsonConvert.DeserializeObject<List<ProductCatalogueProduct>>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)
                );
             
        return products
            .Select(p => new ShoppingCartItem(
              int.Parse(p.ProductId),
              p.ProductName,
              p.ProductDescription,
              p.Price
          ));
    }

    private class ProductCatalogueProduct {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Money Price { get; set; }
    }
}
