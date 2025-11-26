/**
 * Document utilities for Blazouter.
 * Provides type-safe access to document properties and DOM manipulation.
 */
/**
 * Sets the document title
 * @param title - The new document title
 */
export function setTitle(title) {
    if (typeof document !== 'undefined') {
        document.title = title;
    }
}
/**
 * Gets the current document title
 */
export function getTitle() {
    return typeof document !== 'undefined' ? document.title : '';
}
/**
 * Sets a meta tag value
 * @param name - The meta tag name (e.g., "description", "keywords")
 * @param content - The content value
 */
export function setMetaTag(name, content) {
    if (typeof document === 'undefined')
        return;
    let metaTag = document.querySelector(`meta[name="${name}"]`);
    if (!metaTag) {
        metaTag = document.createElement('meta');
        metaTag.name = name;
        document.head.appendChild(metaTag);
    }
    metaTag.content = content;
}
/**
 * Gets a meta tag value
 * @param name - The meta tag name
 */
export function getMetaTag(name) {
    if (typeof document === 'undefined')
        return null;
    const metaTag = document.querySelector(`meta[name="${name}"]`);
    return metaTag ? metaTag.content : null;
}
/**
 * Removes a meta tag
 * @param name - The meta tag name to remove
 */
export function removeMetaTag(name) {
    if (typeof document === 'undefined')
        return;
    const metaTag = document.querySelector(`meta[name="${name}"]`);
    if (metaTag) {
        metaTag.remove();
    }
}
/**
 * Sets an Open Graph meta tag
 * @param property - The Open Graph property (e.g., "og:title", "og:description")
 * @param content - The content value
 */
export function setOpenGraphTag(property, content) {
    if (typeof document === 'undefined')
        return;
    let metaTag = document.querySelector(`meta[property="${property}"]`);
    if (!metaTag) {
        metaTag = document.createElement('meta');
        metaTag.setAttribute('property', property);
        document.head.appendChild(metaTag);
    }
    metaTag.content = content;
}
/**
 * Sets the canonical URL for the page
 * @param url - The canonical URL
 */
export function setCanonicalUrl(url) {
    if (typeof document === 'undefined')
        return;
    let linkTag = document.querySelector('link[rel="canonical"]');
    if (!linkTag) {
        linkTag = document.createElement('link');
        linkTag.rel = 'canonical';
        document.head.appendChild(linkTag);
    }
    linkTag.href = url;
}
/**
 * Focuses an element by selector
 * @param selector - The CSS selector for the element to focus
 */
export function focusElement(selector) {
    if (typeof document === 'undefined')
        return false;
    const element = document.querySelector(selector);
    if (element) {
        element.focus();
        return true;
    }
    return false;
}
/**
 * Scrolls to the top of the page
 * @param smooth - Whether to use smooth scrolling
 */
export function scrollToTop(smooth = true) {
    if (typeof window === 'undefined')
        return;
    window.scrollTo({
        top: 0,
        left: 0,
        behavior: smooth ? 'smooth' : 'auto'
    });
}
/**
 * Scrolls to an element by selector
 * @param selector - The CSS selector for the element to scroll to
 * @param smooth - Whether to use smooth scrolling
 */
export function scrollToElement(selector, smooth = true) {
    if (typeof document === 'undefined')
        return false;
    const element = document.querySelector(selector);
    if (element) {
        element.scrollIntoView({
            behavior: smooth ? 'smooth' : 'auto',
            block: 'start'
        });
        return true;
    }
    return false;
}
/**
 * Gets the document's current scroll position
 */
export function getScrollPosition() {
    if (typeof window === 'undefined')
        return { x: 0, y: 0 };
    return {
        x: window.scrollX || window.pageXOffset,
        y: window.scrollY || window.pageYOffset
    };
}
/**
 * Sets the document's scroll position
 * @param x - The horizontal scroll position
 * @param y - The vertical scroll position
 * @param smooth - Whether to use smooth scrolling
 */
export function setScrollPosition(x, y, smooth = false) {
    if (typeof window === 'undefined')
        return;
    window.scrollTo({
        left: x,
        top: y,
        behavior: smooth ? 'smooth' : 'auto'
    });
}
/**
 * Checks if an element is visible in the viewport
 * @param selector - The CSS selector for the element
 */
export function isElementVisible(selector) {
    if (typeof document === 'undefined')
        return false;
    const element = document.querySelector(selector);
    if (!element)
        return false;
    const rect = element.getBoundingClientRect();
    return (rect.top >= 0 &&
        rect.left >= 0 &&
        rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
        rect.right <= (window.innerWidth || document.documentElement.clientWidth));
}
/**
 * Adds a CSS class to an element
 * @param selector - The CSS selector for the element
 * @param className - The class name to add
 */
export function addClass(selector, className) {
    if (typeof document === 'undefined')
        return false;
    const element = document.querySelector(selector);
    if (element) {
        element.classList.add(className);
        return true;
    }
    return false;
}
/**
 * Removes a CSS class from an element
 * @param selector - The CSS selector for the element
 * @param className - The class name to remove
 */
export function removeClass(selector, className) {
    if (typeof document === 'undefined')
        return false;
    const element = document.querySelector(selector);
    if (element) {
        element.classList.remove(className);
        return true;
    }
    return false;
}
/**
 * Toggles a CSS class on an element
 * @param selector - The CSS selector for the element
 * @param className - The class name to toggle
 */
export function toggleClass(selector, className) {
    if (typeof document === 'undefined')
        return false;
    const element = document.querySelector(selector);
    if (element) {
        element.classList.toggle(className);
        return true;
    }
    return false;
}
/**
 * Gets the document's ready state
 */
export function getReadyState() {
    return typeof document !== 'undefined' ? document.readyState : 'loading';
}
/**
 * Checks if the document is fully loaded
 */
export function isDocumentReady() {
    return typeof document !== 'undefined' && document.readyState === 'complete';
}
// Initialize the document helpers for direct access from C#
if (typeof window !== 'undefined') {
    window.blazouterDocument = {
        setTitle,
        getTitle,
        setMetaTag,
        getMetaTag,
        removeMetaTag,
        setOpenGraphTag,
        setCanonicalUrl,
        focusElement,
        scrollToTop,
        scrollToElement,
        getScrollPosition,
        setScrollPosition,
        isElementVisible,
        addClass,
        removeClass,
        toggleClass,
        getReadyState,
        isDocumentReady
    };
}
