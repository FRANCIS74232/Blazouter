namespace Blazouter.Models
{
    /// <summary>
    /// Provides extension methods for the <see cref="RouteTransition"/> enumeration.
    /// </summary>
    /// <remarks>
    /// These extension methods facilitate the conversion of RouteTransition enum values to their
    /// corresponding CSS class names, enabling seamless integration with the Blazouter CSS animations.
    /// </remarks>
    public static class RouteTransitionExtensions
    {
        /// <summary>
        /// Converts a <see cref="RouteTransition"/> enum value to its corresponding CSS class name.
        /// </summary>
        /// <param name="transition">The route transition to convert.</param>
        /// <returns>
        /// A string containing the CSS class name for the transition, or an empty string if the transition
        /// is <see cref="RouteTransition.None"/> or an unrecognized value.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The returned CSS class name is used by the Router and RouterOutlet components to apply
        /// the appropriate transition animation. The CSS classes are defined in the Blazouter stylesheet
        /// (blazouter.css) that must be included in the application for transitions to work correctly.
        /// </para>
        /// <para>
        /// The CSS class names follow a consistent naming convention where the enum name is converted
        /// to lowercase kebab-case (e.g., SlideUp becomes "slide-up", SlideFade becomes "slide-fade").
        /// The returned class name is prefixed with "transition-" by the Router component when applied
        /// to route containers.
        /// </para>
        /// <para>
        /// <strong>Usage in Blazouter:</strong> You typically don't need to call this method directly
        /// in your application code. The Router and RouterOutlet components automatically call it
        /// internally when applying transitions. However, it can be useful for:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Custom routing implementations</description></item>
        /// <item><description>Manual CSS class management</description></item>
        /// <item><description>Testing and validation</description></item>
        /// <item><description>Debugging transition issues</description></item>
        /// </list>
        /// <para>
        /// <strong>Custom Transitions:</strong> You can create custom CSS animations by:
        /// 1. Creating CSS classes matching the naming convention (e.g., "transition-my-custom")
        /// 2. The CSS must define enter/exit animations using keyframes or CSS transitions
        /// 3. Note that custom enum values would require extending this method
        /// </para>
        /// </remarks>
        /// <example>
        /// Basic usage showing all transition mappings:
        /// <code>
        /// RouteTransition.Pop.ToCssClass()       // Returns "pop"
        /// RouteTransition.Blur.ToCssClass()      // Returns "blur"
        /// RouteTransition.Fade.ToCssClass()      // Returns "fade"
        /// RouteTransition.Flip.ToCssClass()      // Returns "flip"
        /// RouteTransition.Lift.ToCssClass()      // Returns "lift"
        /// RouteTransition.Scale.ToCssClass()     // Returns "scale"
        /// RouteTransition.Slide.ToCssClass()     // Returns "slide"
        /// RouteTransition.Swipe.ToCssClass()     // Returns "swipe"
        /// RouteTransition.Reveal.ToCssClass()    // Returns "reveal"
        /// RouteTransition.Rotate.ToCssClass()    // Returns "rotate"
        /// RouteTransition.Curtain.ToCssClass()   // Returns "curtain"
        /// RouteTransition.SlideUp.ToCssClass()   // Returns "slide-up"
        /// RouteTransition.SlideFade.ToCssClass() // Returns "slide-fade"
        /// RouteTransition.Spotlight.ToCssClass() // Returns "spotlight"
        /// RouteTransition.None.ToCssClass()      // Returns string.Empty
        /// </code>
        /// </example>
        /// <example>
        /// Using in custom route configuration logic:
        /// <code>
        /// var route = new RouteConfig
        /// {
        ///     Path = "/about",
        ///     Component = typeof(AboutPage),
        ///     Transition = RouteTransition.Fade
        /// };
        /// 
        /// // Get the CSS class for custom styling or logging
        /// string cssClass = route.Transition.ToCssClass();
        /// Console.WriteLine($"Transition class: transition-{cssClass}");
        /// </code>
        /// </example>
        /// <example>
        /// Conditional transitions based on user preferences:
        /// <code>
        /// // Respect user's motion preferences
        /// RouteTransition GetTransitionForRoute(bool reducedMotion)
        /// {
        ///     if (reducedMotion)
        ///         return RouteTransition.None;
        ///     
        ///     return RouteTransition.Fade;
        /// }
        /// 
        /// var transition = GetTransitionForRoute(userPreferences.ReduceMotion);
        /// var cssClass = transition.ToCssClass(); // "" or "fade"
        /// </code>
        /// </example>
        public static string ToCssClass(this RouteTransition transition)
        {
            return transition switch
            {
                RouteTransition.Pop => "pop",
                RouteTransition.Blur => "blur",
                RouteTransition.Fade => "fade",
                RouteTransition.Flip => "flip",
                RouteTransition.Lift => "lift",
                RouteTransition.Scale => "scale",
                RouteTransition.Slide => "slide",
                RouteTransition.Swipe => "swipe",
                RouteTransition.Reveal => "reveal",
                RouteTransition.Rotate => "rotate",
                RouteTransition.Curtain => "curtain",
                RouteTransition.SlideUp => "slide-up",
                RouteTransition.SlideFade => "slide-fade",
                RouteTransition.Spotlight => "spotlight",
                RouteTransition.None => string.Empty,
                _ => string.Empty
            };
        }
    }
}