namespace Blazouter.Attributes
{
    /// <summary>
    /// Specifies the layout component for a route.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute allows declarative configuration of the layout component for routes. Layouts
    /// provide a consistent structure (navigation, headers, footers, sidebars) across multiple pages
    /// while allowing page content to vary. The layout component wraps the route's component and
    /// renders it in its @Body section.
    /// </para>
    /// <para>
    /// <strong>Layout Resolution:</strong> The router determines which layout to use in this order:
    /// </para>
    /// <list type="number">
    /// <item><description>If [RouteLayout(typeof(MyLayout))] is specified, use that layout</description></item>
    /// <item><description>If [RouteLayout(null)] is specified, use no layout (component renders alone)</description></item>
    /// <item><description>If no attribute specified, use the Router's DefaultLayout parameter</description></item>
    /// <item><description>If DefaultLayout is also null, component renders without any layout</description></item>
    /// </list>
    /// <para>
    /// The layout component must inherit from LayoutComponentBase and implement the standard Blazor
    /// layout pattern with an @Body directive where the route content will be rendered.
    /// </para>
    /// <para>
    /// Common use cases for different layouts:
    /// </para>
    /// <list type="bullet">
    /// <item><description><strong>Public vs. Admin:</strong> Different navigation and styling for admin sections</description></item>
    /// <item><description><strong>Authentication:</strong> Logged-in vs. logged-out layouts</description></item>
    /// <item><description><strong>Modals/Dialogs:</strong> No layout for overlay pages (use null)</description></item>
    /// <item><description><strong>Print views:</strong> Simplified layout for printing</description></item>
    /// <item><description><strong>Mobile vs. Desktop:</strong> Responsive layout variations</description></item>
    /// <item><description><strong>Feature sections:</strong> Distinct layouts for major app sections</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// Use a specific layout for admin pages:
    /// <code>
    /// [Route("/admin")]
    /// [RouteLayout(typeof(AdminLayout))]
    /// public class AdminPage : ComponentBase
    /// {
    ///     // Renders inside AdminLayout
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// No layout for full-screen pages:
    /// <code>
    /// [Route("/presentation")]
    /// [RouteLayout(null)]
    /// public class PresentationMode : ComponentBase
    /// {
    ///     // Renders without any layout wrapper
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Different layouts for different sections:
    /// <code>
    /// [Route("/")]
    /// [RouteLayout(typeof(MainLayout))]
    /// public class HomePage : ComponentBase { }
    /// 
    /// [Route("/admin/dashboard")]
    /// [RouteLayout(typeof(AdminLayout))]
    /// public class AdminDashboard : ComponentBase { }
    /// 
    /// [Route("/auth/login")]
    /// [RouteLayout(typeof(AuthLayout))]
    /// public class LoginPage : ComponentBase { }
    /// 
    /// [Route("/print/report")]
    /// [RouteLayout(null)]
    /// public class PrintReport : ComponentBase { }
    /// </code>
    /// </example>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RouteLayoutAttribute"/> class.
    /// </remarks>
    /// <param name="layoutType">The type of the layout component. Must inherit from LayoutComponentBase.</param>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class RouteLayoutAttribute(Type? layoutType) : Attribute
    {
        /// <summary>
        /// Gets the layout component type.
        /// </summary>
        /// <value>
        /// A Type that inherits from LayoutComponentBase, or null to explicitly use no layout.
        /// </value>
        /// <remarks>
        /// <para>
        /// When null is specified, it explicitly opts out of using any layout, even if the Router
        /// has a DefaultLayout configured. This is different from not specifying the attribute at all,
        /// which would use the DefaultLayout.
        /// </para>
        /// <para>
        /// The layout type must inherit from Microsoft.AspNetCore.Components.LayoutComponentBase and
        /// contain an @Body directive where the route content will be rendered.
        /// </para>
        /// </remarks>
        public Type? LayoutType { get; } = layoutType;
    }
}