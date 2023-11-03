namespace _03_UseMapWhenMiddleware;

/// <summary> Метод расширения для встраивания middleware </summary>
public static class MyMiddlewareExtensions {
    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder app) {
        return app.UseMiddleware<MyMiddleware>();
    }
}
