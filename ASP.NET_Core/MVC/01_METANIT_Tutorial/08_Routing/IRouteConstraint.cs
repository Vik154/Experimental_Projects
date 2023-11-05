namespace _08_Routing;

/*
httpContext:    объект HttpContext, который инкапсулирует информацию о HTTP-запросе
route:          объект IRouter, который представляет маршрут, в рамках которого применяется ограничение
routeKey:       объект String - название параметра маршрута, к которому применяется ограничение
values:         объект RouteValueDictionary, который представляет набор параметров маршрута в виде словаря,
                       где ключи - названия параметров, а значения - значения параметров маршрута
routeDirection: объект перечисления RouteDirection, которое указывает, 
                       применяется ограничение при обработке запроса, либо при генерации ссылки
 */

public interface IRouteConstraint {
    bool Match(HttpContext? httpContext,
            IRouter? route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection);
}
