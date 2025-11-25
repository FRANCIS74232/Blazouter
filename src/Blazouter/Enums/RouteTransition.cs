namespace Blazouter.Enums
{
    /// <summary>
    /// Defines the available transition animations for route navigation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Route transitions provide visual feedback when navigating between routes, creating a smoother
    /// user experience. Transitions are implemented using CSS animations and can be customized through
    /// CSS classes.
    /// </para>
    /// <para>
    /// Transitions can be enabled/disabled at the Router component level using the EnableTransitions parameter.
    /// Each route can specify its own transition, or use None to disable transitions for that specific route.
    /// </para>
    /// </remarks>
    public enum RouteTransition
    {
        /// <summary>
        /// No transition animation. The new route appears immediately without any visual effect.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Disables all transition animations, causing instant content replacement without any visual effects.
        /// This is the fastest option as it requires no CSS animations or GPU processing. The new route
        /// simply replaces the old one immediately, similar to traditional multi-page application navigation.
        /// </para>
        /// <para>
        /// Characteristics:
        /// <list type="bullet">
        /// <item><description>Zero animation overhead - instant rendering</description></item>
        /// <item><description>No GPU usage for transitions</description></item>
        /// <item><description>Optimal performance on low-end devices</description></item>
        /// <item><description>Traditional web navigation feel</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Best used for:
        /// <list type="bullet">
        /// <item><description>Performance-critical applications or low-end devices</description></item>
        /// <item><description>Accessibility requirements where animations cause issues</description></item>
        /// <item><description>Routes requiring immediate display (error pages, alerts)</description></item>
        /// <item><description>Server-rendered applications mimicking traditional navigation</description></item>
        /// <item><description>Respecting user's prefers-reduced-motion preferences</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Consider using None as a fallback when prefers-reduced-motion is enabled, or for users who
        /// have explicitly disabled animations in your application settings.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/error",
        ///     Component = typeof(ErrorPage),
        ///     Transition = RouteTransition.None // Error pages should appear immediately
        /// }
        /// </code>
        /// </example>
        None,

        /// <summary>
        /// Pop transition. Content appears with a bounce effect, scaling from smaller to larger with elastic easing.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Creates a playful, energetic entrance animation where content pops into view with a bouncing effect.
        /// The animation uses scale transformation combined with elastic easing to create a spring-like motion
        /// that overshoots slightly before settling. This gives the interface a lively, responsive feel.
        /// </para>
        /// <para>
        /// Animation characteristics:
        /// <list type="bullet">
        /// <item><description>Starts at small scale (0.8) and bounces to full size</description></item>
        /// <item><description>Elastic or spring-based easing for natural bounce</description></item>
        /// <item><description>Quick duration (0.4s-0.5s) for snappy responsiveness</description></item>
        /// <item><description>Slight overshoot creates engaging visual feedback</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Best used for:
        /// <list type="bullet">
        /// <item><description>Notifications and alerts that need attention</description></item>
        /// <item><description>Dialog boxes and confirmations</description></item>
        /// <item><description>Playful, casual interfaces</description></item>
        /// <item><description>Actions that should feel responsive and immediate</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Avoid for professional or formal applications where a more subtle transition would be appropriate.
        /// The bouncing effect can feel excessive if overused.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/notification",
        ///     Component = typeof(NotificationDialog),
        ///     Transition = RouteTransition.Pop
        /// }
        /// </code>
        /// </example>
        Pop,

        /// <summary>
        /// Blur transition. Content fades in while transitioning from blurred to sharp focus.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Creates a sophisticated entrance where content materializes from a blurred state into sharp focus.
        /// The animation uses CSS blur filter combined with opacity changes to simulate a camera focusing effect.
        /// This transition provides a modern, premium feel similar to depth-of-field photography effects.
        /// </para>
        /// <para>
        /// Animation characteristics:
        /// <list type="bullet">
        /// <item><description>Starts with strong blur (8px-12px) and zero/low opacity</description></item>
        /// <item><description>Gradually removes blur while increasing opacity</description></item>
        /// <item><description>Smooth, continuous transition (0.5s-0.6s)</description></item>
        /// <item><description>Ease-out timing creates natural deceleration</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Best used for:
        /// <list type="bullet">
        /// <item><description>Image galleries and media-rich content</description></item>
        /// <item><description>Premium or luxury brand applications</description></item>
        /// <item><description>Content where visual quality is emphasized</description></item>
        /// <item><description>Creating a sense of clarity or revelation</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// <strong>Performance Note:</strong> CSS blur filter can be GPU-intensive. Test on target
        /// devices, especially mobile, to ensure smooth performance. Consider using sparingly on
        /// pages with complex layouts.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/portfolio/:id",
        ///     Component = typeof(PortfolioDetail),
        ///     Transition = RouteTransition.Blur
        /// }
        /// </code>
        /// </example>
        Blur,

        /// <summary>
        /// Fade in animation. The new route gradually appears from transparent to opaque.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Creates a simple, elegant opacity transition where content smoothly fades into view. This is
        /// one of the most universally applicable transitions, providing smooth visual feedback without
        /// being distracting or overwhelming. The content transitions from completely transparent (opacity: 0)
        /// to fully visible (opacity: 1) over a short duration.
        /// </para>
        /// <para>
        /// Animation characteristics:
        /// <list type="bullet">
        /// <item><description>Pure opacity transition from 0 to 1</description></item>
        /// <item><description>No spatial movement or transformation</description></item>
        /// <item><description>Short duration (0.3s-0.4s) for quick, responsive feel</description></item>
        /// <item><description>Linear or ease timing for smooth, predictable motion</description></item>
        /// <item><description>Minimal GPU usage - highly performant</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Best used for:
        /// <list type="bullet">
        /// <item><description>Default transition for most applications</description></item>
        /// <item><description>Professional, business, or corporate interfaces</description></item>
        /// <item><description>When subtlety is important</description></item>
        /// <item><description>Content-heavy pages where transitions shouldn't distract</description></item>
        /// <item><description>Applications targeting all skill levels and ages</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Recommended as the default transition for general use. It's universally understood, accessible,
        /// performant, and appropriate for virtually any context. The fade provides just enough visual
        /// feedback to indicate a page change without drawing excessive attention.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/about",
        ///     Component = typeof(AboutPage),
        ///     Transition = RouteTransition.Fade // Safe, professional default
        /// }
        /// </code>
        /// </example>
        Fade,

        /// <summary>
        /// Flip transition. Content rotates in 3D space, flipping from back to front like a card being turned over.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Creates a dramatic 3D flip animation where content appears to rotate around its Y-axis, revealing
        /// itself like a card being flipped over. The animation uses CSS 3D transforms with perspective to
        /// create depth, making the content appear to exist in three-dimensional space. This transition is
        /// eye-catching and creates a strong sense of transformation.
        /// </para>
        /// <para>
        /// Animation characteristics:
        /// <list type="bullet">
        /// <item><description>Rotates 180 degrees around Y-axis (vertical flip)</description></item>
        /// <item><description>Uses CSS perspective for realistic 3D depth</description></item>
        /// <item><description>Moderate duration (0.6s-0.8s) to show the full flip motion</description></item>
        /// <item><description>Ease-in-out timing for smooth acceleration and deceleration</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Best used for:
        /// <list type="bullet">
        /// <item><description>Card-based interfaces or flip cards</description></item>
        /// <item><description>Revealing hidden information or "other side" content</description></item>
        /// <item><description>Toggle between two related views (front/back, question/answer)</description></item>
        /// <item><description>Educational or quiz applications</description></item>
        /// <item><description>Creating memorable, distinctive transitions</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// <strong>Accessibility Note:</strong> The 3D rotation can be disorienting for some users.
        /// Consider providing reduced-motion alternatives for users with vestibular disorders or
        /// motion sensitivity.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/flashcard/:id",
        ///     Component = typeof(FlashcardView),
        ///     Transition = RouteTransition.Flip
        /// }
        /// </code>
        /// </example>
        Flip,

        /// <summary>
        /// Lift transition. Content lifts up with subtle scaling and shadow, mimicking iOS modal presentation.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Inspired by iOS modal presentation, this transition creates a lifting effect where content
        /// appears to rise into view with a slight scale animation and shadow. The combination of
        /// translateY, scale, and box-shadow creates depth and dimension, making the content feel
        /// like it's floating above the previous layer.
        /// </para>
        /// <para>
        /// Visual characteristics:
        /// <list type="bullet">
        /// <item><description>Subtle upward movement (10px) with scale (0.98 to 1)</description></item>
        /// <item><description>Shadow effect that fades during animation</description></item>
        /// <item><description>Quick, snappy timing (0.35s) for responsive feel</description></item>
        /// <item><description>Ease-out easing for natural deceleration</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Best used for:
        /// <list type="bullet">
        /// <item><description>Modal dialogs and overlays</description></item>
        /// <item><description>Pop-up forms or settings panels</description></item>
        /// <item><description>Content that should appear above the current context</description></item>
        /// <item><description>Creating a sense of elevation and importance</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/settings",
        ///     Component = typeof(SettingsModal),
        ///     Transition = RouteTransition.Lift,
        ///     Layout = null // Often used without layout for modal effect
        /// }
        /// </code>
        /// </example>
        Lift,

        /// <summary>
        /// Scale in animation. The new route grows from a smaller size to full size.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Creates a zoom-in effect where content appears to grow from a smaller size to its full dimensions,
        /// creating a sense of content emerging or expanding into view. The animation typically starts at
        /// a reduced scale (0.8-0.95) and smoothly grows to full size (scale: 1), often combined with
        /// opacity fade for a more polished effect.
        /// </para>
        /// <para>
        /// Animation characteristics:
        /// <list type="bullet">
        /// <item><description>Scale transformation from 0.9-0.95 to 1.0</description></item>
        /// <item><description>Often combined with opacity fade (0 to 1)</description></item>
        /// <item><description>Center-origin scaling maintains visual balance</description></item>
        /// <item><description>Short to medium duration (0.3s-0.5s)</description></item>
        /// <item><description>Ease-out timing for natural deceleration</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Best used for:
        /// <list type="bullet">
        /// <item><description>Modal dialogs and popups that need attention</description></item>
        /// <item><description>Important announcements or alerts</description></item>
        /// <item><description>Detail views when zooming from a list item</description></item>
        /// <item><description>Image galleries or media lightboxes</description></item>
        /// <item><description>Emphasizing important page transitions</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// The scaling effect naturally draws the eye and creates focus, making it ideal for content
        /// that should capture immediate attention. Use sparingly to maintain its impact - overuse
        /// can make the interface feel busy or overwhelming.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/announcement",
        ///     Component = typeof(AnnouncementModal),
        ///     Transition = RouteTransition.Scale,
        ///     Layout = null // Often used for modals without layout
        /// }
        /// </code>
        /// </example>
        Scale,

        /// <summary>
        /// Slide from left animation. The new route slides in from the left side of the screen.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Creates a horizontal sliding motion where content enters from the left edge of the viewport,
        /// moving rightward to its final position. This transition provides clear directional feedback
        /// and is widely used in hierarchical navigation patterns. The left-to-right motion is intuitive
        /// for LTR (left-to-right) languages and suggests forward progression through content.
        /// </para>
        /// <para>
        /// Animation characteristics:
        /// <list type="bullet">
        /// <item><description>Horizontal translation from left edge (translateX -100% to 0)</description></item>
        /// <item><description>Smooth, continuous lateral motion</description></item>
        /// <item><description>Medium duration (0.3s-0.5s) for clear directional feedback</description></item>
        /// <item><description>Ease-out timing for natural deceleration</description></item>
        /// <item><description>Sometimes combined with subtle opacity fade</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Best used for:
        /// <list type="bullet">
        /// <item><description>Hierarchical navigation (list to detail views)</description></item>
        /// <item><description>Sequential steps in wizards or multi-step forms</description></item>
        /// <item><description>Drilling down into nested content</description></item>
        /// <item><description>Forward navigation in chronological content</description></item>
        /// <item><description>Master-detail patterns in responsive layouts</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Pairs exceptionally well with opposite slide-right animations for back navigation, creating
        /// a consistent spatial model that helps users maintain mental context. This bidirectional
        /// pairing makes the navigation feel natural and predictable.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/categories/:id/items/:itemId",
        ///     Component = typeof(ItemDetail),
        ///     Transition = RouteTransition.Slide // Forward into detail
        /// }
        /// </code>
        /// </example>
        Slide,

        /// <summary>
        /// Swipe reveal transition. Content swipes in from right to left with a mobile app feel.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Mimics the native swipe gestures found in iOS and Android applications. The content slides
        /// in from the right edge, creating a familiar and intuitive mobile-first experience. Uses
        /// cubic-bezier easing for natural motion that matches platform conventions.
        /// </para>
        /// <para>
        /// Key characteristics:
        /// <list type="bullet">
        /// <item><description>Mobile-optimized timing and easing</description></item>
        /// <item><description>Right-to-left directionality suggests forward navigation</description></item>
        /// <item><description>Smooth, natural motion matching native apps</description></item>
        /// <item><description>Subtle opacity fade enhances the swipe effect</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Perfect for mobile-first applications or any scenario where you want to provide a native
        /// app-like experience. Particularly effective for drill-down navigation patterns (e.g., list
        /// to detail views).
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/products/:id",
        ///     Component = typeof(ProductDetailPage),
        ///     Transition = RouteTransition.Swipe
        /// }
        /// </code>
        /// </example>
        Swipe,

        /// <summary>
        /// Reveal mask transition. Content is revealed through a mask opening from bottom to top.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A modern, sophisticated transition using clip-path animations. Creates a cinematic effect
        /// where content appears as if a mask is being lifted from bottom to top. This transition
        /// provides a premium feel similar to Framer Motion animations.
        /// </para>
        /// <para>
        /// Best suited for hero sections, featured content, or any scenario where you want to create
        /// a strong visual impression. The mask effect draws attention and creates anticipation as
        /// the content is progressively revealed.
        /// </para>
        /// <para>
        /// <strong>Performance Note:</strong> Uses CSS clip-path which is hardware-accelerated in modern
        /// browsers, ensuring smooth animation even on mobile devices.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/showcase",
        ///     Component = typeof(ShowcasePage),
        ///     Transition = RouteTransition.Reveal
        /// }
        /// </code>
        /// </example>
        Reveal,

        /// <summary>
        /// Rotate transition. Content rotates into view along the Z-axis, creating a spinning entrance effect.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Creates a dynamic spinning animation where content rotates around its center point, entering
        /// the viewport with a circular motion. The animation typically combines rotation with opacity fade
        /// and sometimes scale for a more complete transformation. This transition is attention-grabbing
        /// and adds playful energy to the interface.
        /// </para>
        /// <para>
        /// Animation characteristics:
        /// <list type="bullet">
        /// <item><description>Rotates around Z-axis (0 to 360 degrees or partial rotation)</description></item>
        /// <item><description>Often starts at smaller scale (0.8-0.9) growing to full size</description></item>
        /// <item><description>Synchronized opacity fade from transparent to opaque</description></item>
        /// <item><description>Medium-fast duration (0.4s-0.6s) for clear motion</description></item>
        /// <item><description>Ease-out timing for smooth deceleration</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Best used for:
        /// <list type="bullet">
        /// <item><description>Loading screens or spinners transitioning to content</description></item>
        /// <item><description>Game-like interfaces or entertainment applications</description></item>
        /// <item><description>Notifications or alerts that need strong attention</description></item>
        /// <item><description>Playful, creative applications</description></item>
        /// <item><description>Refresh or reload actions with visual feedback</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// <strong>Accessibility Note:</strong> Rotation can cause discomfort for users with motion
        /// sensitivity or vestibular disorders. Always respect prefers-reduced-motion settings and
        /// consider providing alternative transitions for accessibility.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/game/level/:id",
        ///     Component = typeof(GameLevel),
        ///     Transition = RouteTransition.Rotate
        /// }
        /// </code>
        /// </example>
        Rotate,

        /// <summary>
        /// Curtain transition. Content appears as a curtain opening from top to bottom.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Creates a theatrical curtain-opening effect where the content is revealed from top to bottom.
        /// The animation uses clip-path to simulate a curtain being drawn, providing an elegant and
        /// distinctive transition that stands out from standard animations.
        /// </para>
        /// <para>
        /// Ideal for:
        /// <list type="bullet">
        /// <item><description>Presentation or showcase pages</description></item>
        /// <item><description>Gallery or portfolio sections</description></item>
        /// <item><description>Landing pages with dramatic reveals</description></item>
        /// <item><description>Content that deserves a theatrical introduction</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// The curtain effect provides directional feedback (top-to-bottom), making it suitable for
        /// hierarchical navigation where content "opens up" to reveal details.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/gallery",
        ///     Component = typeof(GalleryPage),
        ///     Transition = RouteTransition.Curtain
        /// }
        /// </code>
        /// </example>
        Curtain,

        /// <summary>
        /// Slide from bottom animation. The new route slides up from the bottom of the screen.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Creates an upward sliding motion where content emerges from the bottom edge of the screen,
        /// similar to mobile bottom sheets or modal drawers. The animation provides clear directional
        /// feedback and is commonly used for overlaying content without completely replacing the current
        /// view. The vertical motion naturally suggests temporary or secondary content.
        /// </para>
        /// <para>
        /// Animation characteristics:
        /// <list type="bullet">
        /// <item><description>Slides from bottom edge (translateY 100% to 0)</description></item>
        /// <item><description>Smooth, linear vertical motion</description></item>
        /// <item><description>Medium duration (0.3s-0.4s) for responsive feel</description></item>
        /// <item><description>Often combined with slight opacity fade</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Best used for:
        /// <list type="bullet">
        /// <item><description>Modal dialogs and bottom sheets</description></item>
        /// <item><description>Filter panels and settings drawers</description></item>
        /// <item><description>Secondary navigation or action menus</description></item>
        /// <item><description>Comments sections or additional details</description></item>
        /// <item><description>Mobile-first interfaces mimicking native patterns</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Particularly effective on mobile devices where it matches native gesture patterns.
        /// Users intuitively understand that content sliding up from bottom can be dismissed downward.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/filters",
        ///     Component = typeof(FilterPanel),
        ///     Transition = RouteTransition.SlideUp,
        ///     Layout = null // Often used without layout for overlay effect
        /// }
        /// </code>
        /// </example>
        SlideUp,

        /// <summary>
        /// Slide fade transition. Content slides in from left while simultaneously fading from transparent to opaque.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Combines horizontal sliding motion with opacity fade for a smooth, compound transition effect.
        /// The dual animation creates a more sophisticated entrance than either effect alone, adding depth
        /// and polish to the navigation experience. The synchronized slide and fade movements work together
        /// to draw attention without being jarring.
        /// </para>
        /// <para>
        /// Animation characteristics:
        /// <list type="bullet">
        /// <item><description>Horizontal slide motion (typically from left, -30px to 0)</description></item>
        /// <item><description>Simultaneous opacity fade (0 to 1)</description></item>
        /// <item><description>Both animations synchronized with same duration</description></item>
        /// <item><description>Smooth ease-out timing for natural deceleration</description></item>
        /// <item><description>Subtle, professional appearance suitable for any context</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Best used for:
        /// <list type="bullet">
        /// <item><description>Standard page transitions in business applications</description></item>
        /// <item><description>Content that needs smooth but noticeable transitions</description></item>
        /// <item><description>Dashboard panels or admin interfaces</description></item>
        /// <item><description>When you want more sophistication than plain fade</description></item>
        /// <item><description>Sequential content reveals (list items, cards)</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Strikes an excellent balance between subtle and noticeable, making it a versatile choice
        /// for most professional applications. Less dramatic than pure slide, more engaging than pure fade.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/dashboard/:section",
        ///     Component = typeof(DashboardSection),
        ///     Transition = RouteTransition.SlideFade
        /// }
        /// </code>
        /// </example>
        SlideFade,

        /// <summary>
        /// Spotlight transition. Content fades in with brightness and blur effects, like a spotlight turning on.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Creates a dramatic lighting effect where content appears as if illuminated by a spotlight.
        /// The animation combines brightness adjustment and blur filters to simulate light gradually
        /// focusing on the content. Starts dim and blurred, then brightens and sharpens to full clarity.
        /// </para>
        /// <para>
        /// Animation stages:
        /// <list type="number">
        /// <item><description>Starts at 50% brightness with 6px blur</description></item>
        /// <item><description>Gradually increases brightness to 100%</description></item>
        /// <item><description>Simultaneously removes blur for crisp focus</description></item>
        /// <item><description>Fade-in opacity enhances the lighting effect</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Ideal for:
        /// <list type="bullet">
        /// <item><description>Feature highlights or important announcements</description></item>
        /// <item><description>Search results or filtered content</description></item>
        /// <item><description>Content that should immediately grab attention</description></item>
        /// <item><description>Creating cinematic, high-impact entrances</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// <strong>Performance Note:</strong> Uses CSS filters which may be computationally expensive
        /// on older devices. Test performance on target devices if using extensively.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// new RouteConfig
        /// {
        ///     Path = "/featured",
        ///     Component = typeof(FeaturedContent),
        ///     Transition = RouteTransition.Spotlight,
        ///     Title = "Featured Content"
        /// }
        /// </code>
        /// </example>
        Spotlight
    }
}