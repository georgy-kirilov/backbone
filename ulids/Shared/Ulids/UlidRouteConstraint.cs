using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Shared.Ulids;

internal partial class UlidRouteConstraint : IRouteConstraint
{
    public const string Pattern = "^[0123456789ABCDEFGHJKMNPQRSTVWXYZ]{26}$";
    public const string Culture = "en-US";

    private static readonly Regex UlidRegex = CreateUlidRegex();

    public bool Match(
        HttpContext? httpContext,
        IRouter? route,
        string routeKey,
        RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out var value) && value is string stringValue)
        {
            return UlidRegex.IsMatch(stringValue);
        }

        return false;
    }

    [GeneratedRegex(Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled, Culture)]
    private static partial Regex CreateUlidRegex();
}
