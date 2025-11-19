using Blazouter.Models;
using Blazouter.Services;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Blazouter.Extensions
{
    /// <summary>
    /// Provides extension methods for RouteConfig collections to support attribute-based route discovery.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RouteConfigExtensions simplifies working with attribute-based routes by providing fluent extension
    /// methods that integrate with the RouteAttributeDiscoveryService. These methods enable declarative
    /// route definition through attributes while maintaining compatibility with programmatic route configuration.
    /// </para>
    /// <para>
    /// The extensions support two primary scenarios:
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// <b>AddAttributeRoutes</b> - Adds discovered routes to an existing list, allowing mixed programmatic
    /// and attribute-based configuration
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <b>FromAttributes</b> - Creates a new route list entirely from attributes, simplifying configuration
    /// when all routes use attributes
    /// </description>
    /// </item>
    /// </list>
    /// <para>
    /// These methods use reflection to scan assemblies at startup, which provides a convenient developer
    /// experience but has implications for trimmed/AOT scenarios. Always consider the tradeoffs between
    /// convenience and trimming compatibility for your deployment target.
    /// </para>
    /// </remarks>
    /// <example>
    /// Mix programmatic and attribute-based routes:
    /// <code>
    /// private List&lt;RouteConfig&gt; _routes = new List&lt;RouteConfig&gt;
    /// {
    ///     // Programmatic routes first
    ///     new RouteConfig 
    ///     { 
    ///         Path = "/", 
    ///         Component = typeof(Home),
    ///         Exact = true 
    ///     },
    ///     new RouteConfig 
    ///     { 
    ///         Path = "/about", 
    ///         Component = typeof(About) 
    ///     }
    /// }
    /// .AddAttributeRoutes(typeof(App).Assembly); // Add attribute-based routes
    /// </code>
    /// </example>
    public static class RouteConfigExtensions
    {
        /// <summary>
        /// Discovers and adds routes from components decorated with route attributes in the specified assemblies.
        /// </summary>
        /// <param name="routes">
        /// The existing list of routes to add discovered routes to. This list is modified in-place by adding
        /// new route configurations discovered through attribute scanning.
        /// </param>
        /// <param name="assemblies">
        /// One or more assemblies to scan for components with route attributes. Each assembly is scanned for
        /// all types that inherit from ComponentBase and have a [Route] attribute.
        /// </param>
        /// <returns>
        /// The same route list (fluent interface) with discovered routes added, allowing method chaining.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This extension method provides a fluent way to combine programmatic and attribute-based route
        /// configuration. It scans the specified assemblies for components with route attributes and appends
        /// them to the existing route list, maintaining the order of routes in the collection.
        /// </para>
        /// <para>
        /// Route order matters in Blazouter - routes are matched in the order they appear in the list, with
        /// the first matching route being used. When mixing programmatic and attribute-based routes, consider
        /// the final ordering:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Routes added programmatically appear first (more specific, higher priority)</description></item>
        /// <item><description>Attribute-based routes are appended (more general, lower priority)</description></item>
        /// <item><description>If calling AddAttributeRoutes multiple times, routes are added in call order</description></item>
        /// </list>
        /// <para>
        /// This method uses RouteAttributeDiscoveryService internally, which scans for all supported route
        /// attributes including [Route], [RouteGuard], [RouteTransition], [RouteLayout], and others.
        /// </para>
        /// <para>
        /// <strong>Performance Note:</strong> Assembly scanning is performed each time this method is called.
        /// If calling with the same assemblies multiple times, consider caching the discovered routes or
        /// calling once during initialization.
        /// </para>
        /// <para>
        /// <strong>Trimming Warning:</strong> This method requires reflection and may not work correctly in
        /// trimmed or AOT-compiled applications unless proper trim warnings are preserved.
        /// </para>
        /// </remarks>
        /// <example>
        /// Build routes with programmatic and attribute-based configuration:
        /// <code>
        /// // In App.razor.cs or Routes.razor.cs
        /// private List&lt;RouteConfig&gt; _routes = new List&lt;RouteConfig&gt;
        /// {
        ///     // High-priority programmatic routes
        ///     new RouteConfig 
        ///     { 
        ///         Path = "/",
        ///         Component = typeof(HomePage),
        ///         Exact = true,
        ///         Transition = RouteTransition.Fade
        ///     },
        ///     
        ///     // Redirect old path
        ///     new RouteConfig
        ///     {
        ///         Path = "/old-home",
        ///         RedirectTo = "/"
        ///     }
        /// }
        /// // Add all attribute-based routes from main assembly
        /// .AddAttributeRoutes(typeof(App).Assembly)
        /// 
        /// // Also scan shared component library
        /// .AddAttributeRoutes(typeof(SharedLib.BaseComponent).Assembly);
        /// </code>
        /// </example>
        /// <example>
        /// Conditional route addition based on configuration:
        /// <code>
        /// private List&lt;RouteConfig&gt; BuildRoutes()
        /// {
        ///     var routes = new List&lt;RouteConfig&gt;
        ///     {
        ///         new RouteConfig { Path = "/", Component = typeof(Home) }
        ///     };
        ///     
        ///     // Always add routes from main assembly
        ///     routes.AddAttributeRoutes(typeof(App).Assembly);
        ///     
        ///     // Conditionally add plugin routes
        ///     if (_configuration.PluginsEnabled)
        ///     {
        ///         routes.AddAttributeRoutes(typeof(PluginBase).Assembly);
        ///     }
        ///     
        ///     return routes;
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="FromAttributes(Assembly[])"/>
        /// <seealso cref="RouteAttributeDiscoveryService.DiscoverRoutes(Assembly[])"/>
        [RequiresUnreferencedCode("Scanning for route attributes requires unreferenced code. Types might be removed during trimming.")]
        public static List<RouteConfig> AddAttributeRoutes(this List<RouteConfig> routes, params Assembly[] assemblies)
        {
            List<RouteConfig> discoveredRoutes = RouteAttributeDiscoveryService.DiscoverRoutes(assemblies);

            routes.AddRange(discoveredRoutes);

            return routes;
        }

        /// <summary>
        /// Creates a new list of routes from components decorated with route attributes in the specified assemblies.
        /// </summary>
        /// <param name="assemblies">
        /// One or more assemblies to scan for components with route attributes. Each assembly is fully scanned
        /// for all types that inherit from ComponentBase and have a [Route] attribute.
        /// </param>
        /// <returns>
        /// A new list of <see cref="RouteConfig"/> objects created from discovered route attributes. Returns
        /// an empty list if no components with route attributes are found in the specified assemblies.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This static method provides a convenient way to create a route configuration entirely from attributes,
        /// eliminating the need for any programmatic route configuration when all routes can be defined
        /// declaratively on components.
        /// </para>
        /// <para>
        /// This approach is ideal for applications where:
        /// </para>
        /// <list type="bullet">
        /// <item><description>All routes are simple enough to be defined via attributes</description></item>
        /// <item><description>Route configuration should live alongside component code</description></item>
        /// <item><description>You want minimal boilerplate in your routing setup</description></item>
        /// <item><description>Routes don't require complex conditional logic</description></item>
        /// </list>
        /// <para>
        /// For applications that need to mix programmatic and attribute-based configuration, use
        /// <see cref="AddAttributeRoutes"/> instead, which allows adding attribute-based routes to an
        /// existing programmatically configured list.
        /// </para>
        /// <para>
        /// The method scans for all supported route attributes including [Route], [RouteGuard],
        /// [RouteTransition], [RouteLayout], [RouteTitle], [RouteData], [RouteRedirect], and [RouteExact].
        /// Multiple assemblies can be scanned, and routes from all assemblies are combined into a single list.
        /// </para>
        /// <para>
        /// <strong>Trimming Warning:</strong> This method uses reflection and may not work correctly in
        /// trimmed or AOT-compiled applications. Ensure route attributes are preserved in trim configuration.
        /// </para>
        /// </remarks>
        /// <example>
        /// Pure attribute-based routing configuration:
        /// <code>
        /// // In App.razor.cs or Routes.razor.cs
        /// @code {
        ///     private List&lt;RouteConfig&gt; _routes = RouteConfigExtensions.FromAttributes(
        ///         typeof(App).Assembly
        ///     );
        /// }
        /// 
        /// // All routes defined via attributes on components:
        /// [Route("/")]
        /// [RouteTransition(RouteTransition.Fade)]
        /// public class HomePage : ComponentBase { }
        /// 
        /// [Route("/about")]
        /// public class AboutPage : ComponentBase { }
        /// 
        /// [Route("/admin")]
        /// [RouteGuard(typeof(AuthGuard))]
        /// [RouteLayout(typeof(AdminLayout))]
        /// public class AdminPage : ComponentBase { }
        /// </code>
        /// </example>
        /// <example>
        /// Scan multiple assemblies including plugin assemblies:
        /// <code>
        /// // Scan main app assembly plus any loaded plugin assemblies
        /// var assemblies = new List&lt;Assembly&gt; { typeof(App).Assembly };
        /// assemblies.AddRange(PluginLoader.GetPluginAssemblies());
        /// 
        /// private List&lt;RouteConfig&gt; _routes = RouteConfigExtensions.FromAttributes(
        ///     assemblies.ToArray()
        /// );
        /// </code>
        /// </example>
        /// <example>
        /// Cache discovered routes for performance:
        /// <code>
        /// public static class RoutesCache
        /// {
        ///     private static List&lt;RouteConfig&gt;? _cachedRoutes;
        ///     
        ///     public static List&lt;RouteConfig&gt; GetRoutes()
        ///     {
        ///         if (_cachedRoutes == null)
        ///         {
        ///             _cachedRoutes = RouteConfigExtensions.FromAttributes(
        ///                 typeof(Program).Assembly
        ///             );
        ///         }
        ///         return _cachedRoutes;
        ///     }
        /// }
        /// 
        /// // In Router component
        /// private List&lt;RouteConfig&gt; _routes = RoutesCache.GetRoutes();
        /// </code>
        /// </example>
        /// <seealso cref="AddAttributeRoutes(List{RouteConfig}, Assembly[])"/>
        /// <seealso cref="RouteAttributeDiscoveryService.DiscoverRoutes(Assembly[])"/>
        [RequiresUnreferencedCode("Scanning for route attributes requires unreferenced code. Types might be removed during trimming.")]
        public static List<RouteConfig> FromAttributes(params Assembly[] assemblies)
        {
            return RouteAttributeDiscoveryService.DiscoverRoutes(assemblies);
        }
    }
}