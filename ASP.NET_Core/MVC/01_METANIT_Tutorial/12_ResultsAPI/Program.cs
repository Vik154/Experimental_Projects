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

        app.Run();
    }
}
