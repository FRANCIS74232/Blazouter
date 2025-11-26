using Microsoft.JSInterop;

namespace Blazouter.Interops
{
    /// <summary>
    /// Provides JavaScript interop for document manipulation operations.
    /// </summary>
    /// <remarks>
    /// This service wraps browser document and DOM manipulation functions with type-safe C# methods,
    /// using the TypeScript-defined JavaScript interop functions.
    /// </remarks>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DocumentInterop"/> class.
    /// </remarks>
    /// <param name="jsRuntime">The JavaScript runtime for interop.</param>
    public class DocumentInterop(IJSRuntime jsRuntime)
    {
        /// <summary>
        /// Sets the document title.
        /// </summary>
        /// <param name="title">The new document title to display in the browser tab.</param>
        /// <remarks>
        /// This is particularly useful for updating the page title based on the current route.
        /// </remarks>
        /// <example>
        /// <code>
        /// await documentInterop.SetTitleAsync("Home - My App");
        /// </code>
        /// </example>
        public async Task SetTitleAsync(string title)
        {
            await jsRuntime.InvokeVoidAsync("blazouterDocument.setTitle", title);
        }

        /// <summary>
        /// Gets the current document title.
        /// </summary>
        /// <returns>The current document title.</returns>
        public async Task<string> GetTitleAsync()
        {
            return await jsRuntime.InvokeAsync<string>("blazouterDocument.getTitle");
        }

        /// <summary>
        /// Sets a meta tag value.
        /// </summary>
        /// <param name="name">The meta tag name (e.g., "description", "keywords").</param>
        /// <param name="content">The content value.</param>
        /// <remarks>
        /// If the meta tag doesn't exist, it will be created. If it exists, its content will be updated.
        /// This is useful for SEO and social media sharing.
        /// </remarks>
        /// <example>
        /// <code>
        /// await documentInterop.SetMetaTagAsync("description", "Welcome to our website");
        /// await documentInterop.SetMetaTagAsync("keywords", "blazor, routing, spa");
        /// </code>
        /// </example>
        public async Task SetMetaTagAsync(string name, string content)
        {
            await jsRuntime.InvokeVoidAsync("blazouterDocument.setMetaTag", name, content);
        }

        /// <summary>
        /// Gets a meta tag value.
        /// </summary>
        /// <param name="name">The meta tag name.</param>
        /// <returns>The meta tag content, or null if the tag doesn't exist.</returns>
        public async Task<string?> GetMetaTagAsync(string name)
        {
            return await jsRuntime.InvokeAsync<string?>("blazouterDocument.getMetaTag", name);
        }

        /// <summary>
        /// Removes a meta tag.
        /// </summary>
        /// <param name="name">The meta tag name to remove.</param>
        public async Task RemoveMetaTagAsync(string name)
        {
            await jsRuntime.InvokeVoidAsync("blazouterDocument.removeMetaTag", name);
        }

        /// <summary>
        /// Sets an Open Graph meta tag.
        /// </summary>
        /// <param name="property">The Open Graph property (e.g., "og:title", "og:description", "og:image").</param>
        /// <param name="content">The content value.</param>
        /// <remarks>
        /// Open Graph tags are used by social media platforms like Facebook, LinkedIn, and Twitter
        /// to display rich previews when your page is shared.
        /// </remarks>
        /// <example>
        /// <code>
        /// await documentInterop.SetOpenGraphTagAsync("og:title", "My Page Title");
        /// await documentInterop.SetOpenGraphTagAsync("og:description", "Page description");
        /// await documentInterop.SetOpenGraphTagAsync("og:image", "https://example.com/image.jpg");
        /// </code>
        /// </example>
        public async Task SetOpenGraphTagAsync(string property, string content)
        {
            await jsRuntime.InvokeVoidAsync("blazouterDocument.setOpenGraphTag", property, content);
        }

        /// <summary>
        /// Sets the canonical URL for the page.
        /// </summary>
        /// <param name="url">The canonical URL.</param>
        /// <remarks>
        /// The canonical URL tells search engines which URL is the preferred version of a page,
        /// helping to avoid duplicate content issues.
        /// </remarks>
        /// <example>
        /// <code>
        /// await documentInterop.SetCanonicalUrlAsync("https://example.com/products/123");
        /// </code>
        /// </example>
        public async Task SetCanonicalUrlAsync(string url)
        {
            await jsRuntime.InvokeVoidAsync("blazouterDocument.setCanonicalUrl", url);
        }

        /// <summary>
        /// Focuses an element by CSS selector.
        /// </summary>
        /// <param name="selector">The CSS selector for the element to focus.</param>
        /// <returns>True if the element was found and focused; otherwise, false.</returns>
        /// <example>
        /// <code>
        /// bool focused = await documentInterop.FocusElementAsync("#search-input");
        /// </code>
        /// </example>
        public async Task<bool> FocusElementAsync(string selector)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterDocument.focusElement", selector);
        }

        /// <summary>
        /// Scrolls to the top of the page.
        /// </summary>
        /// <param name="smooth">Whether to use smooth scrolling animation. Default is true.</param>
        /// <remarks>
        /// This is useful when navigating to a new route to ensure the user starts at the top of the page.
        /// </remarks>
        /// <example>
        /// <code>
        /// await documentInterop.ScrollToTopAsync();
        /// await documentInterop.ScrollToTopAsync(smooth: false); // Instant scroll
        /// </code>
        /// </example>
        public async Task ScrollToTopAsync(bool smooth = true)
        {
            await jsRuntime.InvokeVoidAsync("blazouterDocument.scrollToTop", smooth);
        }

        /// <summary>
        /// Scrolls to an element by CSS selector.
        /// </summary>
        /// <param name="selector">The CSS selector for the element to scroll to.</param>
        /// <param name="smooth">Whether to use smooth scrolling animation. Default is true.</param>
        /// <returns>True if the element was found and scrolled to; otherwise, false.</returns>
        /// <example>
        /// <code>
        /// bool scrolled = await documentInterop.ScrollToElementAsync("#section-2");
        /// </code>
        /// </example>
        public async Task<bool> ScrollToElementAsync(string selector, bool smooth = true)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterDocument.scrollToElement", selector, smooth);
        }

        /// <summary>
        /// Represents a scroll position with X and Y coordinates.
        /// </summary>
        public record ScrollPosition(int X, int Y);

        /// <summary>
        /// Gets the document's current scroll position.
        /// </summary>
        /// <returns>The current scroll position with X and Y coordinates.</returns>
        public async Task<ScrollPosition> GetScrollPositionAsync()
        {
            dynamic result = await jsRuntime.InvokeAsync<dynamic>("blazouterDocument.getScrollPosition");
            return new ScrollPosition((int)result.x, (int)result.y);
        }

        /// <summary>
        /// Sets the document's scroll position.
        /// </summary>
        /// <param name="x">The horizontal scroll position.</param>
        /// <param name="y">The vertical scroll position.</param>
        /// <param name="smooth">Whether to use smooth scrolling animation. Default is false.</param>
        public async Task SetScrollPositionAsync(int x, int y, bool smooth = false)
        {
            await jsRuntime.InvokeVoidAsync("blazouterDocument.setScrollPosition", x, y, smooth);
        }

        /// <summary>
        /// Checks if an element is visible in the viewport.
        /// </summary>
        /// <param name="selector">The CSS selector for the element.</param>
        /// <returns>True if the element is completely visible in the viewport; otherwise, false.</returns>
        public async Task<bool> IsElementVisibleAsync(string selector)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterDocument.isElementVisible", selector);
        }

        /// <summary>
        /// Adds a CSS class to an element.
        /// </summary>
        /// <param name="selector">The CSS selector for the element.</param>
        /// <param name="className">The class name to add.</param>
        /// <returns>True if the element was found and the class was added; otherwise, false.</returns>
        public async Task<bool> AddClassAsync(string selector, string className)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterDocument.addClass", selector, className);
        }

        /// <summary>
        /// Removes a CSS class from an element.
        /// </summary>
        /// <param name="selector">The CSS selector for the element.</param>
        /// <param name="className">The class name to remove.</param>
        /// <returns>True if the element was found and the class was removed; otherwise, false.</returns>
        public async Task<bool> RemoveClassAsync(string selector, string className)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterDocument.removeClass", selector, className);
        }

        /// <summary>
        /// Toggles a CSS class on an element.
        /// </summary>
        /// <param name="selector">The CSS selector for the element.</param>
        /// <param name="className">The class name to toggle.</param>
        /// <returns>True if the element was found and the class was toggled; otherwise, false.</returns>
        public async Task<bool> ToggleClassAsync(string selector, string className)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterDocument.toggleClass", selector, className);
        }

        /// <summary>
        /// Gets the document's ready state.
        /// </summary>
        /// <returns>The document ready state ("loading", "interactive", or "complete").</returns>
        public async Task<string> GetReadyStateAsync()
        {
            return await jsRuntime.InvokeAsync<string>("blazouterDocument.getReadyState");
        }

        /// <summary>
        /// Checks if the document is fully loaded.
        /// </summary>
        /// <returns>True if the document is completely loaded; otherwise, false.</returns>
        public async Task<bool> IsDocumentReadyAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterDocument.isDocumentReady");
        }
    }
}