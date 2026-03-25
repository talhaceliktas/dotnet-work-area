using Example_2.Data;

namespace Example_2.Constraints
{
    public class KartalConstraint : IRouteConstraint
    {
        public bool Match(
        HttpContext? httpContext,
        IRouter? route,
        string routeKey,
        RouteValueDictionary values,
        RouteDirection routeDirection)
        {

            string value = values[routeKey]?.ToString() ?? "";

            return AracData.Araclar.Exists(x=> x.name == value);
            
        }

    }
}
