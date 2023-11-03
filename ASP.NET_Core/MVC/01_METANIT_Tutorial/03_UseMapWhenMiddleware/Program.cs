using System.Reflection.PortableExecutable;

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
        app.MapWhen(UseMapWhenTestPredicate, UseMapWhenTestConfiguration);
        app.Run(UseMapWhenRun);

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
}
