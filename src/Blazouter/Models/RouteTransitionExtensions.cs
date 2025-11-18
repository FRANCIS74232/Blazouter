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
        /// (blazouter.css) that must be included in the application.
        /// </para>
        /// <para>
        /// The CSS class names follow a consistent naming convention where the enum name is converted
        /// to lowercase kebab-case (e.g., SlideUp becomes "slide-up").
        /// </para>
        /// </remarks>
        /// <example>
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