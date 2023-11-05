namespace _08_Routing;

/// <summary> Ограничение для параметра маршрута </summary>
public class SecretCodeConstraint : IRouteConstraint, IParameterPolicy {
   
    string _secretCode;    // допустимый код
    public SecretCodeConstraint(string secretCode) => _secretCode = secretCode;

    public bool Match(HttpContext? httpContext, 
                      IRouter? route, 
                      string routeKey, 
                      RouteValueDictionary values, 
                      RouteDirection routeDirection)
    {
        return values[routeKey]?.ToString() == _secretCode;
    }
}