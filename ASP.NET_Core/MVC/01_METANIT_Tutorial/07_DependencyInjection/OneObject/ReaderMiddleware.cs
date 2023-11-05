namespace _07_DependencyInjection.OneObject;

class ReaderMiddleware {
    IReader _reader;

    public ReaderMiddleware(RequestDelegate _, IReader reader) => _reader = reader;

    public async Task InvokeAsync(HttpContext context) {
        await context.Response.WriteAsync($"Current Value: {_reader.ReadValue()}");
    }
}
