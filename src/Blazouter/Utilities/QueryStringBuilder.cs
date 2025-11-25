using System.Globalization;

namespace Blazouter.Utilities
{
    /// <summary>
    /// Provides a fluent API for building query strings with type-safe parameter addition.
    /// </summary>
    /// <remarks>
    /// <para>
    /// QueryStringBuilder helps construct well-formed query strings without manual URL encoding
    /// or string concatenation. It handles proper encoding of keys and values automatically.
    /// </para>
    /// <para>
    /// The builder supports various data types including strings, numbers, booleans, enums,
    /// dates, and collections. All values are converted to appropriate string representations
    /// and URL-encoded.
    /// </para>
    /// </remarks>
    /// <example>
    /// Build a query string with multiple parameters:
    /// <code>
    /// var queryString = new QueryStringBuilder()
    ///     .Add("search", "blazor")
    ///     .Add("page", 2)
    ///     .Add("active", true)
    ///     .Build();
    /// // Result: "search=blazor&amp;page=2&amp;active=true"
    /// </code>
    /// </example>
    public class QueryStringBuilder
    {
        private readonly Dictionary<string, List<string>> _parameters = [];

        /// <summary>
        /// Adds a string parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        /// <remarks>
        /// If the key already exists, the value is added to support multiple values for the same key.
        /// Both key and value are URL-encoded automatically.
        /// </remarks>
        public QueryStringBuilder Add(string key, string? value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                return this;
            }

            if (!_parameters.ContainsKey(key))
            {
                _parameters[key] = [];
            }

            _parameters[key].Add(value);
            return this;
        }

        /// <summary>
        /// Adds an integer parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, int value)
        {
            return Add(key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a nullable integer parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, int? value)
        {
            return value.HasValue ? Add(key, value.Value) : this;
        }

        /// <summary>
        /// Adds a long integer parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, long value)
        {
            return Add(key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a nullable long integer parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, long? value)
        {
            return value.HasValue ? Add(key, value.Value) : this;
        }

        /// <summary>
        /// Adds a decimal parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, decimal value)
        {
            return Add(key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a nullable decimal parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, decimal? value)
        {
            return value.HasValue ? Add(key, value.Value) : this;
        }

        /// <summary>
        /// Adds a double parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, double value)
        {
            return Add(key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a nullable double parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, double? value)
        {
            return value.HasValue ? Add(key, value.Value) : this;
        }

        /// <summary>
        /// Adds a boolean parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Converted to lowercase "true" or "false".</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, bool value)
        {
            return Add(key, value.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Adds a nullable boolean parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, bool? value)
        {
            return value.HasValue ? Add(key, value.Value) : this;
        }

        /// <summary>
        /// Adds a DateTime parameter to the query string in ISO 8601 format.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        /// <remarks>
        /// The DateTime is formatted using the "O" (round-trip) format specifier for ISO 8601 compliance.
        /// </remarks>
        public QueryStringBuilder Add(string key, DateTime value)
        {
            return Add(key, value.ToString("O", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a nullable DateTime parameter to the query string in ISO 8601 format.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, DateTime? value)
        {
            return value.HasValue ? Add(key, value.Value) : this;
        }

        /// <summary>
        /// Adds a Guid parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, Guid value)
        {
            return Add(key, value.ToString());
        }

        /// <summary>
        /// Adds a nullable Guid parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add(string key, Guid? value)
        {
            return value.HasValue ? Add(key, value.Value) : this;
        }

        /// <summary>
        /// Adds an enum parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The enum value. Converted to its string representation.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Add<TEnum>(string key, TEnum value) where TEnum : struct, Enum
        {
            return Add(key, value.ToString());
        }

        /// <summary>
        /// Sets a string parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        /// <remarks>
        /// Unlike Add(), this method removes any existing values for the key before adding the new value.
        /// Use Set() when you want to replace a parameter value, and Add() when you want to append multiple values.
        /// </remarks>
        /// <example>
        /// <code>
        /// var builder = new QueryStringBuilder()
        ///     .Add("id", 1)
        ///     .Set("id", 2);  // Replaces the previous value
        /// // Result: "id=2" (not "id=1&amp;id=2")
        /// </code>
        /// </example>
        public QueryStringBuilder Set(string key, string? value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                return this;
            }

            _parameters.Remove(key);
            _parameters[key] = [value];
            return this;
        }

        /// <summary>
        /// Sets an integer parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, int value)
        {
            return Set(key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Sets a nullable integer parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, int? value)
        {
            return value.HasValue ? Set(key, value.Value) : this;
        }

        /// <summary>
        /// Sets a long integer parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, long value)
        {
            return Set(key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Sets a nullable long integer parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, long? value)
        {
            return value.HasValue ? Set(key, value.Value) : this;
        }

        /// <summary>
        /// Sets a decimal parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, decimal value)
        {
            return Set(key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Sets a nullable decimal parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, decimal? value)
        {
            return value.HasValue ? Set(key, value.Value) : this;
        }

        /// <summary>
        /// Sets a double parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, double value)
        {
            return Set(key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Sets a nullable double parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, double? value)
        {
            return value.HasValue ? Set(key, value.Value) : this;
        }

        /// <summary>
        /// Sets a boolean parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Converted to lowercase "true" or "false".</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, bool value)
        {
            return Set(key, value.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Sets a nullable boolean parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, bool? value)
        {
            return value.HasValue ? Set(key, value.Value) : this;
        }

        /// <summary>
        /// Sets a DateTime parameter in the query string in ISO 8601 format, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, DateTime value)
        {
            return Set(key, value.ToString("O", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Sets a nullable DateTime parameter in the query string in ISO 8601 format, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, DateTime? value)
        {
            return value.HasValue ? Set(key, value.Value) : this;
        }

        /// <summary>
        /// Sets a Guid parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, Guid value)
        {
            return Set(key, value.ToString());
        }

        /// <summary>
        /// Sets a nullable Guid parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value. Null values are ignored.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set(string key, Guid? value)
        {
            return value.HasValue ? Set(key, value.Value) : this;
        }

        /// <summary>
        /// Sets an enum parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The enum value. Converted to its string representation.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Set<TEnum>(string key, TEnum value) where TEnum : struct, Enum
        {
            return Set(key, value.ToString());
        }

        /// <summary>
        /// Adds multiple values for the same parameter to the query string.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="values">The collection of values to add.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        /// <remarks>
        /// This method is useful for array-like parameters (e.g., "tag=a&amp;tag=b&amp;tag=c").
        /// Null values in the collection are ignored.
        /// </remarks>
        public QueryStringBuilder AddRange(string key, IEnumerable<string>? values)
        {
            if (values == null)
            {
                return this;
            }

            foreach (string? value in values.Where(v => v != null))
            {
                Add(key, value);
            }

            return this;
        }

        /// <summary>
        /// Conditionally adds a parameter to the query string.
        /// </summary>
        /// <param name="condition">If true, the parameter is added; otherwise, it's skipped.</param>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        /// <example>
        /// <code>
        /// var builder = new QueryStringBuilder()
        ///     .AddIf(!string.IsNullOrEmpty(searchTerm), "q", searchTerm)
        ///     .AddIf(page > 1, "page", page);
        /// </code>
        /// </example>
        public QueryStringBuilder AddIf(bool condition, string key, string? value)
        {
            return condition ? Add(key, value) : this;
        }

        /// <summary>
        /// Sets multiple values for the same parameter in the query string, replacing any existing values.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="values">The collection of values to set.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        /// <remarks>
        /// <para>
        /// Unlike AddRange(), this method removes any existing values for the key before adding the new values.
        /// Use SetRange() when you want to replace all parameter values, and AddRange() when you want to append values.
        /// </para>
        /// <para>
        /// This is useful for array-like parameters (e.g., "tag=a&amp;tag=b&amp;tag=c").
        /// Null values in the collection are ignored.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// var builder = new QueryStringBuilder()
        ///     .AddRange("tag", new[] { "a", "b" })
        ///     .SetRange("tag", new[] { "c", "d" });  // Replaces previous values
        /// // Result: "tag=c&amp;tag=d" (not "tag=a&amp;tag=b&amp;tag=c&amp;tag=d")
        /// </code>
        /// </example>
        public QueryStringBuilder SetRange(string key, IEnumerable<string>? values)
        {
            if (values == null)
            {
                return this;
            }

            _parameters.Remove(key);

            foreach (string? value in values.Where(v => v != null))
            {
                Add(key, value);
            }

            return this;
        }

        /// <summary>
        /// Conditionally sets a parameter in the query string, replacing any existing value.
        /// </summary>
        /// <param name="condition">If true, the parameter is set; otherwise, it's skipped.</param>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        /// <remarks>
        /// Unlike AddIf(), this method removes any existing values for the key before adding the new value.
        /// Use SetIf() when you want to replace a parameter value, and AddIf() when you want to append values.
        /// </remarks>
        /// <example>
        /// <code>
        /// var builder = new QueryStringBuilder()
        ///     .Add("page", 1)
        ///     .SetIf(hasNextPage, "page", nextPageNum);  // Replaces previous value if condition is true
        /// </code>
        /// </example>
        public QueryStringBuilder SetIf(bool condition, string key, string? value)
        {
            return condition ? Set(key, value) : this;
        }

        /// <summary>
        /// Removes all parameters with the specified key from the query string.
        /// </summary>
        /// <param name="key">The parameter name to remove.</param>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Remove(string key)
        {
            _parameters.Remove(key);
            return this;
        }

        /// <summary>
        /// Removes all parameters from the query string.
        /// </summary>
        /// <returns>The current QueryStringBuilder instance for method chaining.</returns>
        public QueryStringBuilder Clear()
        {
            _parameters.Clear();
            return this;
        }

        /// <summary>
        /// Builds the final query string from all added parameters.
        /// </summary>
        /// <returns>
        /// A properly formatted and URL-encoded query string without the leading '?' character.
        /// Returns an empty string if no parameters were added.
        /// </returns>
        /// <remarks>
        /// Multiple values for the same key are included as separate parameters (e.g., "key=val1&amp;key=val2").
        /// The order of parameters in the resulting string may not match the order they were added.
        /// </remarks>
        /// <example>
        /// <code>
        /// var queryString = new QueryStringBuilder()
        ///     .Add("search", "blazor routing")
        ///     .Add("page", 2)
        ///     .Build();
        /// // Result: "search=blazor+routing&amp;page=2"
        /// </code>
        /// </example>
        public string Build()
        {
            List<string> parts = [];

            foreach (KeyValuePair<string, List<string>> kvp in _parameters)
            {
                foreach (string value in kvp.Value)
                {
                    parts.Add($"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(value)}");
                }
            }

            return string.Join("&", parts);
        }

        /// <summary>
        /// Builds the final query string and returns it with a leading '?' character.
        /// </summary>
        /// <returns>
        /// A properly formatted and URL-encoded query string with a leading '?' character.
        /// Returns an empty string if no parameters were added.
        /// </returns>
        /// <example>
        /// <code>
        /// var queryString = new QueryStringBuilder()
        ///     .Add("id", 123)
        ///     .BuildWithPrefix();
        /// // Result: "?id=123"
        /// </code>
        /// </example>
        public string BuildWithPrefix()
        {
            string result = Build();
            return string.IsNullOrEmpty(result) ? string.Empty : $"?{result}";
        }

        /// <summary>
        /// Converts the query string to a dictionary representation.
        /// </summary>
        /// <returns>
        /// A dictionary where keys map to the last value added for that key.
        /// For parameters with multiple values, only the last value is included.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method is useful when you need a dictionary representation for compatibility
        /// with APIs that expect Dictionary&lt;string, string&gt; instead of query strings.
        /// </para>
        /// <para>
        /// <strong>Why .Last():</strong> When multiple values exist for the same key, the last value
        /// is used to match the behavior of most web frameworks and query string parsers, which
        /// typically override earlier values with later ones. If you need all values, use the
        /// Build() method and parse the resulting query string, or add values to separate keys.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// var dict = new QueryStringBuilder()
        ///     .Add("id", 1)
        ///     .Add("id", 2)  // This overwrites the previous value in dictionary
        ///     .ToDictionary();
        /// // Result: { "id": "2" }
        /// </code>
        /// </example>
        public Dictionary<string, string> ToDictionary()
        {
            return _parameters.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Last()
            );
        }
    }
}