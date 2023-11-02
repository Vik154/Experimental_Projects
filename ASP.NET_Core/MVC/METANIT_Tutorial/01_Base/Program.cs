namespace _01_Base;

public class Program {
    public static void Main(string[] args) {

        // Для создания объекта WebApplication необходим специальный класс-строитель - WebApplicationBuilder.
        // И в файле Program.cs вначале создается данный объект с помощью статического метода
        // WebApplication.CreateBuilder:
        // В качестве параметра в метод передаются аргументы, которые передаются приложению при запуске.
        var builder = WebApplication.CreateBuilder(args);

        // Получив объект WebApplicationBuilder, у него вызывается метод Build(),
        // который и создает объект WebApplication:
        var app = builder.Build();

        // С помощью объекта WebApplication можно настроить всю инфраструктуру приложения -
        // его конфигурацию, маршруты и так далее. по умолчанию для приложения определяется один маршрут:
        // Метод MapGet() в качестве первого параметра принимает путь, по которому можно обратиться к приложению.
        // В данном случае это путь "/", то есть по сути корень веб-приложения - имя домена и порта,
        // после которых может идти слеш, например, https://localhost:7256/
        app.MapGet("/", () => "Hello World!");

        // И в конце необходимо запустить приложение. Для этого у класса WebApplication вызывается метод Run():
        // Данный метод следует вызывать в самом конце построения конвейера обработки запроса.
        // До него же могут быть помещены другие методы, которые добавляют компоненты middleware.
        // app.Run();

        #region ПОЛУЧЕНИЕ И ОТПРАВКА ОТВЕТОВ / ЗАПРОСОВ
        // app.Run(async (context) => await context.Response.WriteAsync("Hello world from lambda"));
        // app.Run(HelloWorld);

        // MyRequest myRequest = new MyRequest(HelloWorld);
        // app.Run(async c => await myRequest(c));

        // app.Run(ReturnContent);

        /// <summary> Получение заголовка запроса </summary>
        // app.Run(GetHeader);

        /// <summary> Получение пути запроса </summary>
        // app.Run(GetPath);

        /// <summary> Получение строки запроса QueryString </summary>
        // app.Run(GetQueryString);
        #endregion

        app.Run();
    }

    // Тест метода Run() и конвейера middleware
    // В качестве параметра метод Run принимает делегат RequestDelegate. Этот делегат имеет следующее определение:
    // public delegate Task RequestDelegate(HttpContext context);

    /// <summary> Тестирование метода Run() </summary>
    public delegate Task MyRequest(HttpContext context);

    /// <summary> Тестирование метода Run() </summary>
    public static async Task HelloWorld(HttpContext context) {
        await context.Response.WriteAsync($"Hello World from static method");
    }

    /// <summary> Отправка контента в ответе </summary>
    static async Task ReturnContent(HttpContext context) {
        var response = context.Response; 
        response.ContentType = "text/html";
        await response.WriteAsync("<h2>Hello World</h2><h3>Welcome to ASP.NET Core</h3>");
    }

    /// <summary> Получение заголовка запроса </summary>
    static async Task GetHeader(HttpContext context) {
        context.Response.ContentType = "text/html; charset=utf-8";
        var stringBuilder = new System.Text.StringBuilder("<table>");

        foreach (var header in context.Request.Headers) {
            stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
        }
        stringBuilder.Append("</table>");
        await context.Response.WriteAsync(stringBuilder.ToString());
    }

    /// <summary> Получение пути запроса </summary>
    static async Task GetPath(HttpContext context) {
        await context.Response.WriteAsync($"Path: {context.Request.Path}");
    }

    /// <summary> Получение строки запроса QueryString </summary>
    static async Task GetQueryString(HttpContext context) {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync($"<p>Path: {context.Request.Path}</p>" +
            $"<p>QueryString: {context.Request.QueryString}</p>");
    }
}
