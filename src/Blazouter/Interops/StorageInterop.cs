using Microsoft.JSInterop;

namespace Blazouter.Interops
{
    /// <summary>
    /// Provides JavaScript interop for browser storage operations (localStorage and sessionStorage).
    /// </summary>
    /// <remarks>
    /// This service wraps browser storage APIs with type-safe C# methods,
    /// using the TypeScript-defined JavaScript interop functions. Values are automatically
    /// serialized/deserialized to/from JSON.
    /// </remarks>
    /// <remarks>
    /// Initializes a new instance of the <see cref="StorageInterop"/> class.
    /// </remarks>
    /// <param name="jsRuntime">The JavaScript runtime for interop.</param>
    public class StorageInterop(IJSRuntime jsRuntime)
    {
        #region LocalStorage

        /// <summary>
        /// Sets an item in localStorage.
        /// </summary>
        /// <param name="key">The storage key.</param>
        /// <param name="value">The value to store (will be JSON serialized).</param>
        /// <returns>True if the operation succeeded; otherwise, false.</returns>
        /// <example>
        /// <code>
        /// await storageInterop.SetLocalStorageAsync("theme", "dark");
        /// await storageInterop.SetLocalStorageAsync("user", new { Name = "John", Age = 30 });
        /// </code>
        /// </example>
        public async Task<bool> SetLocalStorageAsync(string key, object? value)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterStorage.setLocalStorage", key, value);
        }

        /// <summary>
        /// Gets an item from localStorage.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the value to.</typeparam>
        /// <param name="key">The storage key.</param>
        /// <returns>The deserialized value, or default if the key doesn't exist.</returns>
        /// <example>
        /// <code>
        /// var theme = await storageInterop.GetLocalStorageAsync&lt;string&gt;("theme");
        /// var user = await storageInterop.GetLocalStorageAsync&lt;User&gt;("user");
        /// </code>
        /// </example>
        public async Task<T?> GetLocalStorageAsync<T>(string key)
        {
            return await jsRuntime.InvokeAsync<T?>("blazouterStorage.getLocalStorage", key);
        }

        /// <summary>
        /// Removes an item from localStorage.
        /// </summary>
        /// <param name="key">The storage key to remove.</param>
        /// <returns>True if the operation succeeded; otherwise, false.</returns>
        public async Task<bool> RemoveLocalStorageAsync(string key)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterStorage.removeLocalStorage", key);
        }

        /// <summary>
        /// Clears all items from localStorage.
        /// </summary>
        /// <returns>True if the operation succeeded; otherwise, false.</returns>
        /// <remarks>
        /// Use with caution as this will remove all items from localStorage for the current domain.
        /// </remarks>
        public async Task<bool> ClearLocalStorageAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterStorage.clearLocalStorage");
        }

        /// <summary>
        /// Gets all keys from localStorage.
        /// </summary>
        /// <returns>An array of all storage keys.</returns>
        public async Task<string[]> GetLocalStorageKeysAsync()
        {
            return await jsRuntime.InvokeAsync<string[]>("blazouterStorage.getLocalStorageKeys");
        }

        /// <summary>
        /// Checks if a key exists in localStorage.
        /// </summary>
        /// <param name="key">The storage key to check.</param>
        /// <returns>True if the key exists; otherwise, false.</returns>
        public async Task<bool> HasLocalStorageAsync(string key)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterStorage.hasLocalStorage", key);
        }

        #endregion

        #region SessionStorage

        /// <summary>
        /// Sets an item in sessionStorage.
        /// </summary>
        /// <param name="key">The storage key.</param>
        /// <param name="value">The value to store (will be JSON serialized).</param>
        /// <returns>True if the operation succeeded; otherwise, false.</returns>
        /// <remarks>
        /// Session storage persists only for the duration of the browser session.
        /// Data is cleared when the tab/window is closed.
        /// </remarks>
        /// <example>
        /// <code>
        /// await storageInterop.SetSessionStorageAsync("tempData", someValue);
        /// </code>
        /// </example>
        public async Task<bool> SetSessionStorageAsync(string key, object? value)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterStorage.setSessionStorage", key, value);
        }

        /// <summary>
        /// Gets an item from sessionStorage.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the value to.</typeparam>
        /// <param name="key">The storage key.</param>
        /// <returns>The deserialized value, or default if the key doesn't exist.</returns>
        public async Task<T?> GetSessionStorageAsync<T>(string key)
        {
            return await jsRuntime.InvokeAsync<T?>("blazouterStorage.getSessionStorage", key);
        }

        /// <summary>
        /// Removes an item from sessionStorage.
        /// </summary>
        /// <param name="key">The storage key to remove.</param>
        /// <returns>True if the operation succeeded; otherwise, false.</returns>
        public async Task<bool> RemoveSessionStorageAsync(string key)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterStorage.removeSessionStorage", key);
        }

        /// <summary>
        /// Clears all items from sessionStorage.
        /// </summary>
        /// <returns>True if the operation succeeded; otherwise, false.</returns>
        public async Task<bool> ClearSessionStorageAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterStorage.clearSessionStorage");
        }

        /// <summary>
        /// Gets all keys from sessionStorage.
        /// </summary>
        /// <returns>An array of all storage keys.</returns>
        public async Task<string[]> GetSessionStorageKeysAsync()
        {
            return await jsRuntime.InvokeAsync<string[]>("blazouterStorage.getSessionStorageKeys");
        }

        /// <summary>
        /// Checks if a key exists in sessionStorage.
        /// </summary>
        /// <param name="key">The storage key to check.</param>
        /// <returns>True if the key exists; otherwise, false.</returns>
        public async Task<bool> HasSessionStorageAsync(string key)
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterStorage.hasSessionStorage", key);
        }

        #endregion
    }
}