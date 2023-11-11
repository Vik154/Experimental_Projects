using _01_Base;

namespace _01_BaseHost;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();   // ��������� ��������� Razor Pages

        var app = builder.Build();

        // app.UseBlazorFrameworkFiles() ��������� ���������� ����� ���������� Blazor WebAssembly
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        // app.MapFallbackToFile("index.html") ������������� � �������� �������� �� ���������
        // ���� index.html, ������� ������� �� ������� Blazor WebAssembly.
        // app.MapFallbackToFile("index.html");

        // ������ �� ��������� ������� ����� ������������ ����������, ��������������� �� �������� "_Host.cshtml".
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}
