/**
 * Browser navigation utilities for Blazouter.
 * Provides type-safe access to browser history API.
 */
export interface BrowserNavigationOptions {
    /**
     * Whether to force a full page reload instead of SPA navigation
     */
    forceLoad?: boolean;
}
/**
 * Navigates back in browser history
 */
export declare function goBack(): void;
/**
 * Navigates forward in browser history
 */
export declare function goForward(): void;
/**
 * Navigates to a specific position in browser history
 * @param delta - The number of steps to navigate (negative for back, positive for forward)
 */
export declare function go(delta: number): void;
/**
 * Gets the current history length
 */
export declare function getHistoryLength(): number;
/**
 * Checks if the browser can navigate back
 */
export declare function canGoBack(): boolean;
/**
 * Pushes a new state to browser history without navigation
 * @param state - The state object to push
 * @param title - The title (currently unused by most browsers)
 * @param url - The URL to display
 */
export declare function pushState(state: any, title: string, url?: string): void;
/**
 * Replaces the current state in browser history
 * @param state - The state object to replace
 * @param title - The title (currently unused by most browsers)
 * @param url - The URL to display
 */
export declare function replaceState(state: any, title: string, url?: string): void;
/**
 * Gets the current history state
 */
export declare function getState(): any;
/**
 * Gets the current URL
 */
export declare function getCurrentUrl(): string;
/**
 * Gets the current pathname
 */
export declare function getPathname(): string;
/**
 * Gets the current hash (without the # symbol)
 */
export declare function getHash(): string;
/**
 * Sets the hash (without page reload)
 * @param hash - The hash value (without # symbol)
 */
export declare function setHash(hash: string): void;
/**
 * Gets the current query string (without the ? symbol)
 */
export declare function getQueryString(): string;
/**
 * Gets a query parameter value by name
 * @param name - The parameter name
 */
export declare function getQueryParam(name: string): string | null;
/**
 * Gets all query parameters as an object
 */
export declare function getAllQueryParams(): Record<string, string>;
/**
 * Reloads the current page
 * @param _forceReload - Whether to force reload from server (bypass cache) - Note: Modern browsers may ignore this parameter
 */
export declare function reload(_forceReload?: boolean): void;
