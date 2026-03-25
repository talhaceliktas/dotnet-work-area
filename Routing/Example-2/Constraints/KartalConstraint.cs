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
            string[] list = ["kartal", "siha", "akinci"];

            string value = values[routeKey]?.ToString() ?? "";

            return list.Contains(value);

            
        }

    }
}
