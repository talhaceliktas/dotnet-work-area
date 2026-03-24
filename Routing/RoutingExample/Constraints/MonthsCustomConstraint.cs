namespace RoutingExample.Constraints
{
    // Eg: sales-report/2040/apr

    public class MonthsCustomConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext,
            IRouter? route, string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            string input = values[routeKey]?.ToString();

            string[] months = ["apr", "jul", "oct", "jan"];

            if(months.Contains(input))
                return true;
            return false;
        }
    }
}
