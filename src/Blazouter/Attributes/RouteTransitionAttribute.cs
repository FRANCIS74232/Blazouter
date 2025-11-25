using Blazouter.Enums;

namespace Blazouter.Attributes
{
    /// <summary>
    /// Specifies the transition animation for a route.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute allows declarative configuration of route transitions on component classes.
    /// The specified transition will be applied when navigating to this route, providing visual
    /// feedback and enhancing the user experience during navigation.
    /// </para>
    /// <para>
    /// Transitions are implemented using CSS animations defined in the Blazouter stylesheet.
    /// The Router component automatically applies appropriate CSS classes based on the transition
    /// type specified. Transitions can be globally enabled/disabled using the Router's EnableTransitions
    /// parameter.
    /// </para>
    /// <para>
    /// Available transition types range from simple fades to complex 3D effects. Choose transitions
    /// that match your application's style and target audience:
    /// </para>
    /// <list type="bullet">
    /// <item><description><strong>Fade</strong> - Subtle, professional, universally appropriate</description></item>
    /// <item><description><strong>Slide/SlideUp</strong> - Clear directional feedback, mobile-friendly</description></item>
    /// <item><description><strong>Scale/Pop</strong> - Attention-grabbing, playful</description></item>
    /// <item><description><strong>Blur/Spotlight</strong> - Premium feel, media-focused</description></item>
    /// <item><description><strong>Flip/Rotate</strong> - Dramatic, educational contexts</description></item>
    /// </list>
    /// <para>
    /// <strong>Accessibility Note:</strong> Consider users with motion sensitivity or vestibular disorders.
    /// Respect the prefers-reduced-motion media query and provide fallbacks for users who disable animations.
    /// Use RouteTransition.None for critical routes like error pages that require immediate display.
    /// </para>
    /// </remarks>
    /// <example>
    /// Simple fade transition for content pages:
    /// <code>
    /// [Route("/about")]
    /// [RouteTransition(RouteTransition.Fade)]
    /// public class AboutPage : ComponentBase
    /// {
    ///     // Component implementation
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Slide-up transition for modal-style pages:
    /// <code>
    /// [Route("/filters")]
    /// [RouteTransition(RouteTransition.SlideUp)]
    /// [RouteLayout(null)] // No layout for modal effect
    /// public class FilterPanel : ComponentBase
    /// {
    ///     // Modal-style component
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// No transition for critical alerts:
    /// <code>
    /// [Route("/error")]
    /// [RouteTransition(RouteTransition.None)]
    /// public class ErrorPage : ComponentBase
    /// {
    ///     // Error pages should appear immediately
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RouteTransitionAttribute"/> class.
    /// </remarks>
    /// <param name="transition">The transition type to apply when navigating to this route.</param>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class RouteTransitionAttribute(RouteTransition transition) : Attribute
    {
        /// <summary>
        /// Gets the transition type for this route.
        /// </summary>
        /// <value>
        /// A <see cref="RouteTransition"/> enum value specifying the animation type to use when
        /// navigating to this route.
        /// </value>
        /// <remarks>
        /// The transition value corresponds to a CSS class that will be applied to the route container.
        /// The Blazouter CSS file must be included in your application for transitions to work correctly.
        /// See the RouteTransition enum documentation for detailed descriptions of each transition type.
        /// </remarks>
        public RouteTransition Transition { get; } = transition;
    }
}