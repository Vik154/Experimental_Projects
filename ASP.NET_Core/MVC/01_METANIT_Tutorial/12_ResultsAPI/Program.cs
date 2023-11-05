namespace _12_ResultsAPI;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        /// <summary>
        /// Обе конечные точки продуцируют почти аналогичный ответ: 
        /// также будет отправляться одна и та же строка со статусным кодом 200.
        /// Разница будет заключаться только в отдельных заголовках 
        /// (в частности, метод Results.Text добавляет заголовок "Content-Length").
        /// </summary>
        app.Map("/hello", () => Results.Text("Hello ASP.NET Core"));
        app.Map("/", () => "Hello ASP.NET Core");

        /// <summary> Results API (IResult) отправка ответа в обычных компонентах middleware</summary>
        app.Map("/IResult", async context => {
            await Results.Text("Hello ASP.NET Core").ExecuteAsync(context);
        });

        /// <summary>
        /// Метод Content() отправляет текстовое содержимое и позволяет при этом 
        /// задать тип содержимого и кодировку. Одна из версий метода:
        /// public static IResult Content (string content, string? contentType = default, System.Text.Encoding? contentEncoding = default);
        /// </summary>
        app.Map("/China", () => Results.Content("你好", "text/plain", System.Text.Encoding.Unicode));
        app.Map("/DefaultUTF", () => Results.Content("Hello ASP.NET Core"));

        /// <summary> 
        /// Метод Text() работает аналогичным образом, он также отравляет текст и принимает те же параметры:
        /// public static IResult Text (string content, string? contentType = default, System.Text.Encoding? contentEncoding = default);
        /// </summary>
        app.Map("/Chinese", () => Results.Text("你好", "text/plain", System.Text.Encoding.Unicode));
        app.Map("/DefaultText", () => Results.Text("Hello World"));

        /// <summary> 
        /// Для отправки данных в формате json применяется метод Results.Json():
        /// public static IResult Json (object? data, JsonSerializerOptions? options = default, string? contentType = default, int? statusCode = default);
        /// </summary>
        app.Map("/person", () => Results.Json(new { name = "Tom", age = 37 })); // отправка анонимного объекта
        app.Map("/error", () => Results.Json(new { message = "Unexpected error" }, statusCode: 500));   // отправка ошибок.

        /// <summary>
        /// Для переадресации на локальный адрес в рамках приложения применяется метод LocalRedirect():
        /// public static IResult LocalRedirect (string localUrl, bool permanent = false, bool preserveMethod = false);
        /// </summary>
        app.Map("/old", () => Results.LocalRedirect("/new"));
        app.Map("/new", () => "New Address");

        /// <summary>
        /// Метод Redirect() также осуществляет переадресацию и принимает те же параметры, что и LocalRedirect(), 
        /// только адрес для переадресации может не только локальным, но и внешним:
        /// </summary>
        app.Map("/metanit", () => Results.Redirect("https://metanit.com"));
        app.Map("/home", () => "Hello World");

        /// <summary>
        /// Метод StatusCode() позволяет отправить любой статусный код, числовой код которого 
        /// передается в метод в качестве параметра:
        /// </summary>
        app.Map("/about", () => Results.StatusCode(401));

        /// <summary> Метод NotFound() посылает код 404, уведомляя клиента о том, что ресурс не найден </summary>
        app.Map("/contacts", () => Results.NotFound("Error 404. Invalid address"));

        /// <summary> Unauthorized() посылает код 401, уведомляя пользователя, что он не авторизован для доступа к ресурсу: </ summary>
        app.Map("/contactsto", () => Results.Unauthorized());

        /// <summary> BadRequest() посылает код 400, который говорит о том, что запрос некорректный. </ summary>
        app.Map("/contacts/{age:int}", (int age) => {
            if (age < 18)  
                return Results.BadRequest(new { message = "Invalid age" });
            else
                return Results.Content("Access is available");
        });

        /// <summary> 
        /// Метод Ok() посылает статусный код 200, уведомляя об успешном выполнении запроса. 
        /// В качестве параметра метод принимает отправляемую информацию: 
        /// </ summary>
        app.Map("/abouttwo", () => Results.Ok("Laudate omnes gentes laudate"));
        app.Map("/contactstwo", () => Results.Ok(new { message = "Success!" }));

        /// <summary> Определение своего типа IResult </ summary>
        // отправляем html-код при обращении по пути "/"
        app.Map("/GetHtml", ()
            => Results.Extensions.Html(@"<!DOCTYPE html>
                    <html>
                      <head>
                        <title>HELLO WORLD</title>
                        <meta charset='utf-8' />
                      </head>
                      <body>
                        <h1>Hello World</h1>
                      </body>
                    </html>
                    "
            ));

        app.Run();
    }
}


class HtmlResult : IResult {
    string htmlCode = "";
    public HtmlResult(string htmlCode) => this.htmlCode = htmlCode;

    public async Task ExecuteAsync(HttpContext context) {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync(htmlCode);
    }
}

static class ResultsHtmlExtension {
    public static IResult Html(this IResultExtensions ext, string htmlCode) => new HtmlResult(htmlCode);
}