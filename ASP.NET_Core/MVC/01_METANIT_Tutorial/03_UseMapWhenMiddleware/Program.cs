using Microsoft.AspNetCore.Authentication;

namespace _03_UseMapWhenMiddleware;


public class Program {

    static string date = "";

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        /// <summary> Передача действий по конвейеру пример 1 </summary>
        // app.Use(UseTest1);
        // app.Run(RunTest1);

        /// <summary> Ветвление логики в конвейере с помощью UseWhen </summary>
        // app.UseWhen(UseWhenTestPredicate, UseWhenTestConfiguration);
        // app.Run(UseWhenRun);

        /// <summary> Ветвление логики в конвейере с помощью MapWhen </summary>
        // app.MapWhen(UseMapWhenTestPredicate, UseMapWhenTestConfiguration);
        // app.Run(UseMapWhenRun);

        /// <summary> Ветвление логики с помощью Map; обработка запросов по пути "/time" </summary>
        // app.Map("/time", MapTest);
        // app.Run(RunHelloWorld);

        /// <summary> Вложенные маршруты </summary>
        //app.Map("/home", appBuilder => {
        //    appBuilder.Map("/index", MapIndex);             // middleware для "/home/index"
        //    appBuilder.Map("/about", MapAbout);             // middleware для "/home/about"
        //    appBuilder.Run(async (context) => await         // middleware для "/home"
        //        context.Response.WriteAsync("Home Page"));
        //});
        //app.Run(async (context) => await context.Response.WriteAsync($"Hello MAP"));

        // Для добавления компонента middleware, который представляет класс,
        // в конвейер обработки запроса применяется метод UseMiddleware():
        /// <summary> Компонент middleware представленный классом MyMiddleware </summary>
        // app.UseMiddleware<MyMiddleware>();
        // app.Run(async c => await c.Response.WriteAsync("Hello middleware"));

        /// <summary> Метод расширения для встраивания middleware </summary>
        // app.UseMyMiddleware();
        // app.Run(async c => await c.Response.WriteAsync("Hello extension middleware"));

        /// <summary> Конвейер в действии
        /// Схематично конвейер обработки запроса будет выглядеть следующим образом:
        /// Запрос               --> ExampleErrorHandlingMiddleware (next.Invoke(next)) --> 
        /// Обработка запроса    --> ExampleAuthenticationMiddleware (next.Invoke(next)) -->
        /// Обработка запроса    --> ExampleRoutingMiddleware -->
        /// Корректировка ответа --> ExampleErrorHandlingMiddleware
        /// </summary>
        app.UseMiddleware<ExampleErrorHandlingMiddleware>();
        app.UseMiddleware<ExampleAuthenticationMiddleware>();
        app.UseMiddleware<ExampleRoutingMiddleware>();

        app.Run();
    }


    #region МЕТОД IApplicationBuilder.Use()
    // Метод расширения Use() добавляет компонент middleware, который позволяет передать обработку
    // запроса далее следующим в конвейере компонентам. Он имеет следующие версии
    // public static IApplicationBuilder Use(this IApplicationBuilder app, Func<HttpContext, Func<Task>, Task> middleware);
    // public static IApplicationBuilder Use(this IApplicationBuilder app, Func<HttpContext, RequestDelegate, Task> middleware)

    /*
     В общем случае применение метода Use() выглядит следующим образом:

        app.Use(async (context, next) => {
            // действия перед передачей запроса в следующий middleware
            await next.Invoke();
            // действия после обработки запроса следующим middleware
        });
     */

    /// <summary> Передача действий по конвейеру пример 1 </summary>
    static async Task UseTest1(HttpContext context, Func<Task> next) {
        date = DateTime.Now.ToShortDateString();    // действия перед передачей запроса в следующий middleware
        await next.Invoke();                        // передает обработку запроса следующему компоненту в конвейере
        Console.WriteLine($"Current date: {date}"); // действия после обработки запроса следующим middleware
    }

    /// <summary> Передача действий по конвейеру пример 1 </summary>
    static async Task RunTest1(HttpContext context) {
        await context.Response.WriteAsync($"Date: {date}");
    }

    #endregion

    #region Создание ветки конвейера UseWhen
    // Метод UseWhen() на основании некоторого условия позволяет
    // создать ответвление конвейера при обработке запроса:
    // public static IApplicationBuilder UseWhen (this IApplicationBuilder app,
    //               Func<HttpContext,bool> predicate, Action<IApplicationBuilder> configuration);

    // app.Use(bool:context => a == b, void:config)
    static bool UseWhenTestPredicate(HttpContext context) => context.Request.Path == "/time";

    static void UseWhenTestConfiguration(IApplicationBuilder builder) {
        // логируем данные - выводим на консоль приложения
        builder.Use(async (context, next) => {
            var time = DateTime.Now.ToShortTimeString();
            Console.WriteLine($"Time: {time}");
            await next();   // вызываем следующий middleware
        });

        // отправляем ответ
        builder.Run(async context => {
            var time = DateTime.Now.ToShortTimeString();
            await context.Response.WriteAsync($"Time: {time}");
        });
    }

    static async Task UseWhenRun(HttpContext context) {
        await context.Response.WriteAsync($"Hello World");
    }

    #endregion

    #region Создание ветки конвейера MapWhen
    // Метод MapWhen(), как и метод UseWhen(), на основании некоторого условия
    // позволяет создать ответвление конвейера:
    // public static IApplicationBuilder MapWhen (this IApplicationBuilder app,
    //               Func<HttpContext,bool> predicate, Action<IApplicationBuilder> configuration);


    static bool UseMapWhenTestPredicate(HttpContext context) => context.Request.Path == "/time";

    static void UseMapWhenTestConfiguration(IApplicationBuilder builder) {

        builder.Run(async context => {
            var time = DateTime.Now.ToShortTimeString();
            await context.Response.WriteAsync($"Time: {time}");
        });
    }

    static async Task UseMapWhenRun(HttpContext context) {
        await context.Response.WriteAsync($"Hello World");
    }

    #endregion

    #region Метод Map
    // Метод Map() применяется для создания ветки конвейера, которая будет
    // обрабатывать запрос по определенному пути.
    // public static IApplicationBuilder Map (this IApplicationBuilder app,
    //               string pathMatch, Action<IApplicationBuilder> configuration);

    // В качестве параметра pathMatch метод принимает путь запроса, с которым будет сопоставляться ветка.
    // А параметр configuration представляет делегат, в который передается объект IApplicationBuilder
    // и в котором будет создаваться ветка конвейера.

    /// <summary> Создает ответвление конвейера, которое будет обрабатывать запросы по пути "/time" </summary>
    static void MapTest(IApplicationBuilder builder) {
        var time = DateTime.Now.ToShortTimeString();

        // логгируем данные - выводим на консоль приложения
        builder.Use(async (context, next) => {
            Console.WriteLine($"Time: {time}");
            await next();   // вызываем следующий middleware
        });

        builder.Run(async context => await context.Response.WriteAsync($"Time: {time}"));
    }

    static async Task RunHelloWorld(HttpContext context) => await context.Response.WriteAsync("Hello world");

    /// <summary> Вложенные маршруты </summary>
    static void MapIndex(IApplicationBuilder appBuilder) {
        appBuilder.Run(async context => await context.Response.WriteAsync("Index"));
    }

    /// <summary> Вложенные маршруты </summary>
    static void MapAbout(IApplicationBuilder appBuilder) {
        appBuilder.Run(async context => await context.Response.WriteAsync("About"));
    }

    #endregion
}
