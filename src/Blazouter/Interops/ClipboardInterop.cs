using Microsoft.JSInterop;

namespace Blazouter.Interops
{
    /// <summary>
    /// Provides JavaScript interop for clipboard operations.
    /// </summary>
    /// <remarks>
    /// This service wraps the browser's Clipboard API with type-safe C# methods,
    /// using the TypeScript-defined JavaScript interop functions. Includes fallback
    /// support for older browsers.
    /// </remarks>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ClipboardInterop"/> class.
    /// </remarks>
    /// <param name="jsRuntime">The JavaScript runtime for interop.</param>
    public class ClipboardInterop(IJSRuntime jsRuntime)
    {
        /// <summary>
        /// Copies text to the clipboard.
        /// </summary>
        /// <param name="text">The text to copy.</param>
        /// <returns>True if the copy operation succeeded; otherwise, false.</returns>
        /// <remarks>
        /// This method uses the modern Clipboard API with fallback to the legacy execCommand method
        /// for older browsers. Some browsers may require user interaction (e.g., button click) to allow
        /// clipboard access.
        /// </remarks>
        /// <example>
        /// <code>
        /// // In a button click handler
        /// bool success = await clipboardInterop.CopyTextAsync("Hello, World!");
        /// if (success)
        /// {
        ///     // Show success message
        /// }
        /// </code>
        /// </example>
        public async Task<bool> CopyTextAsync(string text)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterClipboard.copyText", text);
        }

        /// <summary>
        /// Reads text from the clipboard.
        /// </summary>
        /// <returns>The text from the clipboard, or null if the operation failed.</returns>
        /// <remarks>
        /// Reading from the clipboard requires the "clipboard-read" permission. The browser
        /// may prompt the user for permission. This operation may not work in all browsers
        /// or contexts.
        /// </remarks>
        /// <example>
        /// <code>
        /// var text = await clipboardInterop.ReadTextAsync();
        /// if (text != null)
        /// {
        ///     Console.WriteLine($"Clipboard content: {text}");
        /// }
        /// </code>
        /// </example>
        public async Task<string?> ReadTextAsync()
        {
            return await jsRuntime.InvokeAsync<string?>("blazouterClipboard.readText");
        }

        /// <summary>
        /// Checks if the Clipboard API is supported in the current browser.
        /// </summary>
        /// <returns>True if clipboard operations are supported; otherwise, false.</returns>
        /// <remarks>
        /// Use this to check for feature support before attempting clipboard operations.
        /// </remarks>
        /// <example>
        /// <code>
        /// if (await clipboardInterop.IsClipboardSupportedAsync())
        /// {
        ///     // Show copy button
        /// }
        /// else
        /// {
        ///     // Hide copy button or show alternative UI
        /// }
        /// </code>
        /// </example>
        public async Task<bool> IsClipboardSupportedAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterClipboard.isClipboardSupported");
        }

        /// <summary>
        /// Checks if clipboard read permission has been granted.
        /// </summary>
        /// <returns>True if permission is granted; otherwise, false.</returns>
        /// <remarks>
        /// This checks the permission status without requesting permission from the user.
        /// </remarks>
        public async Task<bool> HasClipboardReadPermissionAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterClipboard.hasClipboardReadPermission");
        }

        /// <summary>
        /// Checks if clipboard write permission has been granted.
        /// </summary>
        /// <returns>True if permission is granted; otherwise, false.</returns>
        public async Task<bool> HasClipboardWritePermissionAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterClipboard.hasClipboardWritePermission");
        }
    }
}