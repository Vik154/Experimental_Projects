namespace _00_BlazorFirst;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();

        app.UseStaticFiles();

        // После создания объекта приложения app у него вызывается метод MapBlazorHub(),
        // чтобы связать клиентскую часть приложения с сервером посредством соединения SignalR.
        // Благодаря этом будет происходить обмен сообщениями между сервером и клиентов в режиме реального времени.
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
        app.Run();
    }
}
