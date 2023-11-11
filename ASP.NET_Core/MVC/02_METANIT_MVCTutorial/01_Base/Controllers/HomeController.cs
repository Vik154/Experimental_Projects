using Microsoft.AspNetCore.Mvc;

namespace _01_Base.Controllers;


public class HomeController : Controller {

    public string Index() => "Index View";

    public async Task Info() {
        Response.ContentType = "text/html;charset=utf-8";
        await Response.WriteAsync("<h2>Hello World</h2>");
    }

    public async Task HeaderInfo() {
        Response.ContentType = "text/html;charset=utf-8";
        System.Text.StringBuilder tableBuilder = new("<h2>Request headers</h2><table>");
        foreach (var header in Request.Headers) {
            tableBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
        }
        tableBuilder.Append("</table>");
        await Response.WriteAsync(tableBuilder.ToString());
    }

    [HttpGet]
    public string Get() => "Get";
}
