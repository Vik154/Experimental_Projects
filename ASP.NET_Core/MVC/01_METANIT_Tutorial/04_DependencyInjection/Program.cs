using System.Text;

namespace _04_DependencyInjection;


public class Program {

    /// <summary> IServiceCollection - ��� ���������� ������� </summary>
    static IServiceCollection Services { get; set; }

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        /// <summary> ��������� ���� ���������� �������� </summary>
        Services = builder.Services;    
        var app = builder.Build();

        app.Run(TestServices);
        app.Run();

    }

    #region ���������� ������� IServiceCollection

    /// <summary> ����� ���� ���������� �������� ���������� </summary>
    static async Task TestServices(HttpContext context) {
        var sb = new StringBuilder();
        sb.Append("<h1>��� �������</h1>");
        sb.Append("<table>");
        sb.Append("<tr><th>���</th><th>Lifetime</th><th>����������</th></tr>");
        foreach (var svc in Services) {
            sb.Append("<tr>");
            sb.Append($"<td>{svc.ServiceType.FullName}</td>");
            sb.Append($"<td>{svc.Lifetime}</td>");
            sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        context.Response.ContentType = "text/html;charset=utf-8";
        await context.Response.WriteAsync(sb.ToString());
    }

    #endregion
}
