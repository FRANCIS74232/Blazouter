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
export function goBack(): void {
    if (window.history.length > 1) {
        window.history.back();
    }
}

/**
 * Navigates forward in browser history
 */
export function goForward(): void {
    window.history.forward();
}

/**
 * Navigates to a specific position in browser history
 * @param delta - The number of steps to navigate (negative for back, positive for forward)
 */
export function go(delta: number): void {
    window.history.go(delta);
}

/**
 * Gets the current history length
 */
export function getHistoryLength(): number {
    return window.history.length;
}

/**
 * Checks if the browser can navigate back
 */
export function canGoBack(): boolean {
    return window.history.length > 1;
}

/**
 * Pushes a new state to browser history without navigation
 * @param state - The state object to push
 * @param title - The title (currently unused by most browsers)
 * @param url - The URL to display
 */
export function pushState(state: any, title: string, url?: string): void {
    window.history.pushState(state, title, url);
}

/**
 * Replaces the current state in browser history
 * @param state - The state object to replace
 * @param title - The title (currently unused by most browsers)
 * @param url - The URL to display
 */
export function replaceState(state: any, title: string, url?: string): void {
    window.history.replaceState(state, title, url);
}

/**
 * Gets the current history state
 */
export function getState(): any {
    return window.history.state;
}

/**
 * Gets the current URL
 */
export function getCurrentUrl(): string {
    return typeof window !== 'undefined' ? window.location.href : '';
}

/**
 * Gets the current pathname
 */
export function getPathname(): string {
    return typeof window !== 'undefined' ? window.location.pathname : '';
}

/**
 * Gets the current hash (without the # symbol)
 */
export function getHash(): string {
    return typeof window !== 'undefined' ? window.location.hash.substring(1) : '';
}

/**
 * Sets the hash (without page reload)
 * @param hash - The hash value (without # symbol)
 */
export function setHash(hash: string): void {
    if (typeof window !== 'undefined') {
        window.location.hash = hash;
    }
}

/**
 * Gets the current query string (without the ? symbol)
 */
export function getQueryString(): string {
    return typeof window !== 'undefined' ? window.location.search.substring(1) : '';
}

/**
 * Gets a query parameter value by name
 * @param name - The parameter name
 */
export function getQueryParam(name: string): string | null {
    if (typeof window === 'undefined') return null;
    
    const params = new URLSearchParams(window.location.search);
    return params.get(name);
}

/**
 * Gets all query parameters as an object
 */
export function getAllQueryParams(): Record<string, string> {
    if (typeof window === 'undefined') return {};
    
    const params = new URLSearchParams(window.location.search);
    const result: Record<string, string> = {};
    
    params.forEach((value, key) => {
        result[key] = value;
    });
    
    return result;
}

/**
 * Reloads the current page
 * @param _forceReload - Whether to force reload from server (bypass cache) - Note: Modern browsers may ignore this parameter
 */
export function reload(_forceReload: boolean = false): void {
    if (typeof window !== 'undefined') {
        window.location.reload();
    }
}

// Initialize the navigation helpers for direct access from C#
if (typeof window !== 'undefined') {
    (window as any).blazouterNavigation = {
        goBack,
        goForward,
        go,
        getHistoryLength,
        canGoBack,
        pushState,
        replaceState,
        getState,
        getCurrentUrl,
        getPathname,
        getHash,
        setHash,
        getQueryString,
        getQueryParam,
        getAllQueryParams,
        reload
    };
}