using Blazouter.Services;
using System.Globalization;

namespace Blazouter.Extensions
{
    /// <summary>
    /// Extension methods for RouterStateService to provide typed query parameter access.
    /// </summary>
    /// <remarks>
    /// <para>
    /// These extensions make it easier to retrieve and parse query parameters as specific types
    /// without manual parsing and error handling. They provide safe parsing with default values
    /// for invalid or missing parameters.
    /// </para>
    /// </remarks>
    public static class RouterStateExtensions
    {
        /// <summary>
        /// Retrieves a query parameter as an integer.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <param name="defaultValue">The default value to return if parsing fails or the parameter is missing.</param>
        /// <returns>The parsed integer value, or the default value if parsing fails.</returns>
        /// <example>
        /// <code>
        /// int page = RouterState.GetQueryInt("page", 1);
        /// </code>
        /// </example>
        public static int GetQueryInt(this RouterStateService routerState, string key, int defaultValue = 0)
        {
            string? value = routerState.GetQuery(key);
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result)
                ? result
                : defaultValue;
        }

        /// <summary>
        /// Retrieves a query parameter as a nullable integer.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <returns>The parsed integer value, or null if parsing fails or the parameter is missing.</returns>
        /// <example>
        /// <code>
        /// int? userId = RouterState.GetQueryIntOrNull("userId");
        /// if (userId.HasValue)
        /// {
        ///     // Use userId.Value
        /// }
        /// </code>
        /// </example>
        public static int? GetQueryIntOrNull(this RouterStateService routerState, string key)
        {
            string? value = routerState.GetQuery(key);
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result)
                ? result
                : null;
        }

        /// <summary>
        /// Retrieves a query parameter as a long integer.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <param name="defaultValue">The default value to return if parsing fails or the parameter is missing.</param>
        /// <returns>The parsed long value, or the default value if parsing fails.</returns>
        public static long GetQueryLong(this RouterStateService routerState, string key, long defaultValue = 0)
        {
            string? value = routerState.GetQuery(key);
            return long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out long result)
                ? result
                : defaultValue;
        }

        /// <summary>
        /// Retrieves a query parameter as a nullable long integer.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <returns>The parsed long value, or null if parsing fails or the parameter is missing.</returns>
        public static long? GetQueryLongOrNull(this RouterStateService routerState, string key)
        {
            string? value = routerState.GetQuery(key);
            return long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out long result)
                ? result
                : null;
        }

        /// <summary>
        /// Retrieves a query parameter as a decimal.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <param name="defaultValue">The default value to return if parsing fails or the parameter is missing.</param>
        /// <returns>The parsed decimal value, or the default value if parsing fails.</returns>
        public static decimal GetQueryDecimal(this RouterStateService routerState, string key, decimal defaultValue = 0)
        {
            string? value = routerState.GetQuery(key);
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result)
                ? result
                : defaultValue;
        }

        /// <summary>
        /// Retrieves a query parameter as a nullable decimal.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <returns>The parsed decimal value, or null if parsing fails or the parameter is missing.</returns>
        public static decimal? GetQueryDecimalOrNull(this RouterStateService routerState, string key)
        {
            string? value = routerState.GetQuery(key);
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result)
                ? result
                : null;
        }

        /// <summary>
        /// Retrieves a query parameter as a double.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <param name="defaultValue">The default value to return if parsing fails or the parameter is missing.</param>
        /// <returns>The parsed double value, or the default value if parsing fails.</returns>
        public static double GetQueryDouble(this RouterStateService routerState, string key, double defaultValue = 0)
        {
            string? value = routerState.GetQuery(key);
            return double.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double result)
                ? result
                : defaultValue;
        }

        /// <summary>
        /// Retrieves a query parameter as a nullable double.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <returns>The parsed double value, or null if parsing fails or the parameter is missing.</returns>
        public static double? GetQueryDoubleOrNull(this RouterStateService routerState, string key)
        {
            string? value = routerState.GetQuery(key);
            return double.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double result)
                ? result
                : null;
        }

        /// <summary>
        /// Retrieves a query parameter as a boolean.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <param name="defaultValue">The default value to return if parsing fails or the parameter is missing.</param>
        /// <returns>The parsed boolean value, or the default value if parsing fails.</returns>
        /// <remarks>
        /// Accepts "true", "false", "1", "0", "yes", "no" (case-insensitive).
        /// </remarks>
        /// <example>
        /// <code>
        /// bool isActive = RouterState.GetQueryBool("active", false);
        /// </code>
        /// </example>
        public static bool GetQueryBool(this RouterStateService routerState, string key, bool defaultValue = false)
        {
            string? value = routerState.GetQuery(key);

            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            // Support common boolean representations
            value = value.ToLowerInvariant();
            return value switch
            {
                "true" or "1" or "yes" => true,
                "false" or "0" or "no" => false,
                _ => defaultValue
            };
        }

        /// <summary>
        /// Retrieves a query parameter as a nullable boolean.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <returns>The parsed boolean value, or null if parsing fails or the parameter is missing.</returns>
        /// <remarks>
        /// Accepts "true", "false", "1", "0", "yes", "no", "on", "off" (case-insensitive).
        /// </remarks>
        public static bool? GetQueryBoolOrNull(this RouterStateService routerState, string key)
        {
            string? value = routerState.GetQuery(key);

            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            value = value.ToLowerInvariant();
            return value switch
            {
                "true" or "1" or "yes" or "on" => true,
                "false" or "0" or "no" or "off" => false,
                _ => null
            };
        }

        /// <summary>
        /// Retrieves a query parameter as a DateTime.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <param name="defaultValue">The default value to return if parsing fails or the parameter is missing.</param>
        /// <returns>The parsed DateTime value, or the default value if parsing fails.</returns>
        /// <remarks>
        /// Supports ISO 8601 format and other common date formats using InvariantCulture.
        /// </remarks>
        /// <example>
        /// <code>
        /// DateTime startDate = RouterState.GetQueryDateTime("start", DateTime.Today);
        /// </code>
        /// </example>
        public static DateTime GetQueryDateTime(this RouterStateService routerState, string key, DateTime defaultValue)
        {
            string? value = routerState.GetQuery(key);
            return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime result)
                ? result
                : defaultValue;
        }

        /// <summary>
        /// Retrieves a query parameter as a nullable DateTime.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <returns>The parsed DateTime value, or null if parsing fails or the parameter is missing.</returns>
        /// <remarks>
        /// Supports ISO 8601 format and other common date formats using InvariantCulture.
        /// </remarks>
        public static DateTime? GetQueryDateTimeOrNull(this RouterStateService routerState, string key)
        {
            string? value = routerState.GetQuery(key);
            return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime result)
                ? result
                : null;
        }

        /// <summary>
        /// Retrieves a query parameter as a Guid.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <param name="defaultValue">The default value to return if parsing fails or the parameter is missing.</param>
        /// <returns>The parsed Guid value, or the default value if parsing fails.</returns>
        public static Guid GetQueryGuid(this RouterStateService routerState, string key, Guid defaultValue)
        {
            string? value = routerState.GetQuery(key);
            return Guid.TryParse(value, out Guid result) ? result : defaultValue;
        }

        /// <summary>
        /// Retrieves a query parameter as a nullable Guid.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <returns>The parsed Guid value, or null if parsing fails or the parameter is missing.</returns>
        public static Guid? GetQueryGuidOrNull(this RouterStateService routerState, string key)
        {
            string? value = routerState.GetQuery(key);
            return Guid.TryParse(value, out Guid result) ? result : null;
        }

        /// <summary>
        /// Retrieves a query parameter as an enum value.
        /// </summary>
        /// <typeparam name="TEnum">The enum type to parse.</typeparam>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <param name="defaultValue">The default value to return if parsing fails or the parameter is missing.</param>
        /// <param name="ignoreCase">Whether to ignore case when parsing the enum value.</param>
        /// <returns>The parsed enum value, or the default value if parsing fails.</returns>
        /// <example>
        /// <code>
        /// public enum SortOrder { Ascending, Descending }
        /// 
        /// var sortOrder = RouterState.GetQueryEnum("order", SortOrder.Ascending);
        /// </code>
        /// </example>
        public static TEnum GetQueryEnum<TEnum>(this RouterStateService routerState, string key, TEnum defaultValue, bool ignoreCase = true)
            where TEnum : struct, Enum
        {
            string? value = routerState.GetQuery(key);
            return Enum.TryParse<TEnum>(value, ignoreCase, out TEnum result) ? result : defaultValue;
        }

        /// <summary>
        /// Retrieves a query parameter as a nullable enum value.
        /// </summary>
        /// <typeparam name="TEnum">The enum type to parse.</typeparam>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <param name="ignoreCase">Whether to ignore case when parsing the enum value.</param>
        /// <returns>The parsed enum value, or null if parsing fails or the parameter is missing.</returns>
        public static TEnum? GetQueryEnumOrNull<TEnum>(this RouterStateService routerState, string key, bool ignoreCase = true)
            where TEnum : struct, Enum
        {
            string? value = routerState.GetQuery(key);
            return Enum.TryParse<TEnum>(value, ignoreCase, out TEnum result) ? result : null;
        }

        /// <summary>
        /// Retrieves multiple values for the same query parameter as a string array.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <returns>An array of all values for the specified key, or an empty array if not found.</returns>
        /// <remarks>
        /// <para>
        /// <strong>Limitation:</strong> The current implementation only returns a single value in an array
        /// because RouteMatch stores query parameters as a Dictionary&lt;string, string&gt;. Full multi-value
        /// support (e.g., "?tag=a&amp;tag=b&amp;tag=c") would require modifications to RouteMatch to store
        /// multiple values per key.
        /// </para>
        /// <para>
        /// This method is provided for API consistency and future extensibility. For now, it's functionally
        /// equivalent to calling GetQuery and wrapping the result in an array.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// string[] tags = RouterState.GetQueryArray("tag");
        /// // Currently returns single value, e.g., ["a"]
        /// // Future versions may support multiple values, e.g., ["a", "b", "c"]
        /// </code>
        /// </example>
        public static string[] GetQueryArray(this RouterStateService routerState, string key)
        {
            string? value = routerState.GetQuery(key);
            return value != null ? [value] : [];
        }

        /// <summary>
        /// Checks if a query parameter exists in the query string.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <param name="key">The query parameter name.</param>
        /// <returns>True if the parameter exists (even if empty), false otherwise.</returns>
        /// <example>
        /// <code>
        /// if (RouterState.HasQuery("debug"))
        /// {
        ///     // Enable debug mode
        /// }
        /// </code>
        /// </example>
        public static bool HasQuery(this RouterStateService routerState, string key)
        {
            return routerState.CurrentRoute?.Query.ContainsKey(key) == true;
        }

        /// <summary>
        /// Gets all query parameters as a dictionary.
        /// </summary>
        /// <param name="routerState">The RouterStateService instance.</param>
        /// <returns>A dictionary of all query parameters, or an empty dictionary if none exist.</returns>
        /// <example>
        /// <code>
        /// var allParams = RouterState.GetAllQueryParams();
        /// foreach (var kvp in allParams)
        /// {
        ///     Console.WriteLine($"{kvp.Key} = {kvp.Value}");
        /// }
        /// </code>
        /// </example>
        public static Dictionary<string, string> GetAllQueryParams(this RouterStateService routerState)
        {
            return routerState.CurrentRoute?.Query ?? [];
        }
    }
}