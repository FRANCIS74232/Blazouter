/**
 * Document utilities for Blazouter.
 * Provides type-safe access to document properties and DOM manipulation.
 */
/**
 * Sets the document title
 * @param title - The new document title
 */
export declare function setTitle(title: string): void;
/**
 * Gets the current document title
 */
export declare function getTitle(): string;
/**
 * Sets a meta tag value
 * @param name - The meta tag name (e.g., "description", "keywords")
 * @param content - The content value
 */
export declare function setMetaTag(name: string, content: string): void;
/**
 * Gets a meta tag value
 * @param name - The meta tag name
 */
export declare function getMetaTag(name: string): string | null;
/**
 * Removes a meta tag
 * @param name - The meta tag name to remove
 */
export declare function removeMetaTag(name: string): void;
/**
 * Sets an Open Graph meta tag
 * @param property - The Open Graph property (e.g., "og:title", "og:description")
 * @param content - The content value
 */
export declare function setOpenGraphTag(property: string, content: string): void;
/**
 * Sets the canonical URL for the page
 * @param url - The canonical URL
 */
export declare function setCanonicalUrl(url: string): void;
/**
 * Focuses an element by selector
 * @param selector - The CSS selector for the element to focus
 */
export declare function focusElement(selector: string): boolean;
/**
 * Scrolls to the top of the page
 * @param smooth - Whether to use smooth scrolling
 */
export declare function scrollToTop(smooth?: boolean): void;
/**
 * Scrolls to an element by selector
 * @param selector - The CSS selector for the element to scroll to
 * @param smooth - Whether to use smooth scrolling
 */
export declare function scrollToElement(selector: string, smooth?: boolean): boolean;
/**
 * Gets the document's current scroll position
 */
export declare function getScrollPosition(): {
    x: number;
    y: number;
};
/**
 * Sets the document's scroll position
 * @param x - The horizontal scroll position
 * @param y - The vertical scroll position
 * @param smooth - Whether to use smooth scrolling
 */
export declare function setScrollPosition(x: number, y: number, smooth?: boolean): void;
/**
 * Checks if an element is visible in the viewport
 * @param selector - The CSS selector for the element
 */
export declare function isElementVisible(selector: string): boolean;
/**
 * Adds a CSS class to an element
 * @param selector - The CSS selector for the element
 * @param className - The class name to add
 */
export declare function addClass(selector: string, className: string): boolean;
/**
 * Removes a CSS class from an element
 * @param selector - The CSS selector for the element
 * @param className - The class name to remove
 */
export declare function removeClass(selector: string, className: string): boolean;
/**
 * Toggles a CSS class on an element
 * @param selector - The CSS selector for the element
 * @param className - The class name to toggle
 */
export declare function toggleClass(selector: string, className: string): boolean;
/**
 * Gets the document's ready state
 */
export declare function getReadyState(): DocumentReadyState;
/**
 * Checks if the document is fully loaded
 */
export declare function isDocumentReady(): boolean;
