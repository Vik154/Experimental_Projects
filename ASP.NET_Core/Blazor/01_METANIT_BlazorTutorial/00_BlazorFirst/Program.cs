namespace _00_BlazorFirst;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();

        app.UseStaticFiles();

        // ����� �������� ������� ���������� app � ���� ���������� ����� MapBlazorHub(),
        // ����� ������� ���������� ����� ���������� � �������� ����������� ���������� SignalR.
        // ��������� ���� ����� ����������� ����� ����������� ����� �������� � �������� � ������ ��������� �������.
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
        app.Run();
    }
}
