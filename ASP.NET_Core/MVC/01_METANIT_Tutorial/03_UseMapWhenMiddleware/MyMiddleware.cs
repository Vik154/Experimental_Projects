namespace _03_UseMapWhenMiddleware;

/*
 Суть действия класса заключается в том, что мы получаем из запроса параметр "token".
Если полученный токен равен строке "12345678", то передаем запрос дальше следующему компоненту,
вызвав метод _next.Invoke(). Иначе возвращаем пользователю сообщение об ошибке.
 */

/// <summary> Middleware в виде отдельных классов </summary>
public class MyMiddleware {
    private readonly RequestDelegate _next;

    /// <summary> принимает параметр типа RequestDelegate. 
    /// Через этот параметр можно получить ссылку на тот делегат запроса,
    /// который стоит следующим в конвейере обработки запроса </summary>
    public MyMiddleware(RequestDelegate next) => _next = next;

    /// <summary> Также в классе должен быть определен метод, который должен называться 
    /// либо Invoke, либо InvokeAsync. Причем этот метод должен возвращать объект Task 
    /// и принимать в качестве параметра контекст запроса - объект HttpContext. 
    /// Данный метод собственно и будет обрабатывать запрос </summary>
    public async Task InvokeAsync(HttpContext context) {

        var token = context.Request.Query["token"];
        
        if (token != "12345678") {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Token is invalid");
        }
        else {
            await _next.Invoke(context);
        }
    }
}
