/**
 * Viewport utilities for Blazouter.
 * Provides information about the browser viewport and window dimensions.
 */
/**
 * Gets the current viewport width
 */
export function getViewportWidth() {
    if (typeof window === 'undefined')
        return 0;
    return window.innerWidth || document.documentElement.clientWidth;
}
/**
 * Gets the current viewport height
 */
export function getViewportHeight() {
    if (typeof window === 'undefined')
        return 0;
    return window.innerHeight || document.documentElement.clientHeight;
}
/**
 * Gets the current viewport dimensions
 */
export function getViewportSize() {
    return {
        width: getViewportWidth(),
        height: getViewportHeight()
    };
}
/**
 * Gets the screen width
 */
export function getScreenWidth() {
    if (typeof window === 'undefined' || !window.screen)
        return 0;
    return window.screen.width;
}
/**
 * Gets the screen height
 */
export function getScreenHeight() {
    if (typeof window === 'undefined' || !window.screen)
        return 0;
    return window.screen.height;
}
/**
 * Gets the screen dimensions
 */
export function getScreenSize() {
    return {
        width: getScreenWidth(),
        height: getScreenHeight()
    };
}
/**
 * Gets the device pixel ratio
 */
export function getPixelRatio() {
    if (typeof window === 'undefined')
        return 1;
    return window.devicePixelRatio || 1;
}
/**
 * Checks if the viewport is in portrait orientation
 */
export function isPortrait() {
    return getViewportHeight() > getViewportWidth();
}
/**
 * Checks if the viewport is in landscape orientation
 */
export function isLandscape() {
    return getViewportWidth() > getViewportHeight();
}
/**
 * Gets the screen orientation
 */
export function getOrientation() {
    return isPortrait() ? 'portrait' : 'landscape';
}
/**
 * Checks if the device is mobile-sized (width < 768px)
 */
export function isMobile() {
    return getViewportWidth() < 768;
}
/**
 * Checks if the device is tablet-sized (768px <= width < 1024px)
 */
export function isTablet() {
    const width = getViewportWidth();
    return width >= 768 && width < 1024;
}
/**
 * Checks if the device is desktop-sized (width >= 1024px)
 */
export function isDesktop() {
    return getViewportWidth() >= 1024;
}
/**
 * Gets the device type based on viewport width
 */
export function getDeviceType() {
    if (isMobile())
        return 'mobile';
    if (isTablet())
        return 'tablet';
    return 'desktop';
}
/**
 * Checks if the page is being viewed in fullscreen mode
 */
export function isFullscreen() {
    if (typeof document === 'undefined')
        return false;
    return !!(document.fullscreenElement ||
        document.webkitFullscreenElement ||
        document.mozFullScreenElement ||
        document.msFullscreenElement);
}
/**
 * Requests fullscreen mode for the document
 */
export async function requestFullscreen() {
    if (typeof document === 'undefined')
        return false;
    try {
        const elem = document.documentElement;
        if (elem.requestFullscreen) {
            await elem.requestFullscreen();
        }
        else if (elem.webkitRequestFullscreen) {
            await elem.webkitRequestFullscreen();
        }
        else if (elem.mozRequestFullScreen) {
            await elem.mozRequestFullScreen();
        }
        else if (elem.msRequestFullscreen) {
            await elem.msRequestFullscreen();
        }
        return true;
    }
    catch (e) {
        console.error('Error requesting fullscreen:', e);
        return false;
    }
}
/**
 * Exits fullscreen mode
 */
export async function exitFullscreen() {
    if (typeof document === 'undefined')
        return false;
    try {
        if (document.exitFullscreen) {
            await document.exitFullscreen();
        }
        else if (document.webkitExitFullscreen) {
            await document.webkitExitFullscreen();
        }
        else if (document.mozCancelFullScreen) {
            await document.mozCancelFullScreen();
        }
        else if (document.msExitFullscreen) {
            await document.msExitFullscreen();
        }
        return true;
    }
    catch (e) {
        console.error('Error exiting fullscreen:', e);
        return false;
    }
}
// Initialize the viewport helpers for direct access from C#
if (typeof window !== 'undefined') {
    window.blazouterViewport = {
        getViewportWidth,
        getViewportHeight,
        getViewportSize,
        getScreenWidth,
        getScreenHeight,
        getScreenSize,
        getPixelRatio,
        isPortrait,
        isLandscape,
        getOrientation,
        isMobile,
        isTablet,
        isDesktop,
        getDeviceType,
        isFullscreen,
        requestFullscreen,
        exitFullscreen
    };
}
