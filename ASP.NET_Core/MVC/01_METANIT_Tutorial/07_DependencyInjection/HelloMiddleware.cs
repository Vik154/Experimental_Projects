namespace _07_DependencyInjection;

class HelloMiddleware {

    readonly IEnumerable<IHelloService> _helloServices;

    public HelloMiddleware(RequestDelegate _, IEnumerable<IHelloService> helloServices) {
        _helloServices = helloServices;
    }

    public async Task InvokeAsync(HttpContext context) {
        context.Response.ContentType = "text/html; charset=utf-8";
        string responseText = "";
        foreach (var service in _helloServices) {
            responseText += $"<h3>{service.Message}</h3>";
        }
        await context.Response.WriteAsync(responseText);
    }
}