namespace _07_DependencyInjection.OneObject;

class GeneratorMiddleware {

    RequestDelegate _next;
    IGenerator _generator;

    public GeneratorMiddleware(RequestDelegate next, IGenerator generator) {
        _next = next;
        _generator = generator;
    }

    public async Task InvokeAsync(HttpContext context) {
        if (context.Request.Path == "/generate")
            await context.Response.WriteAsync($"New Value: {_generator.GenerateValue()}");
        else
            await _next.Invoke(context);
    }
}
