namespace _03_UseMapWhenMiddleware;

/// <summary> Класс аутентификации пользователя
/// если в строке запроса есть параметр token и он имеет какое-нибудь значение, 
/// то пользователь аутентифицирован. А если он не аутентифицирован, 
/// то надо необходимо ограничить доступ пользователям к приложению
/// </summary>
public class ExampleAuthenticationMiddleware {

    readonly RequestDelegate _next;
    public ExampleAuthenticationMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context) {
        var token = context.Request.Query["token"];
        if (string.IsNullOrWhiteSpace(token)) {
            context.Response.StatusCode = 403;
        }
        else {
            await _next.Invoke(context);
        }
    }
}
