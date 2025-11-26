/**
 * Viewport utilities for Blazouter.
 * Provides information about the browser viewport and window dimensions.
 */

/**
 * Gets the current viewport width
 */
export function getViewportWidth(): number {
    if (typeof window === 'undefined') return 0;
    return window.innerWidth || document.documentElement.clientWidth;
}

/**
 * Gets the current viewport height
 */
export function getViewportHeight(): number {
    if (typeof window === 'undefined') return 0;
    return window.innerHeight || document.documentElement.clientHeight;
}

/**
 * Gets the current viewport dimensions
 */
export function getViewportSize(): { width: number; height: number } {
    return {
        width: getViewportWidth(),
        height: getViewportHeight()
    };
}

/**
 * Gets the screen width
 */
export function getScreenWidth(): number {
    if (typeof window === 'undefined' || !window.screen) return 0;
    return window.screen.width;
}

/**
 * Gets the screen height
 */
export function getScreenHeight(): number {
    if (typeof window === 'undefined' || !window.screen) return 0;
    return window.screen.height;
}

/**
 * Gets the screen dimensions
 */
export function getScreenSize(): { width: number; height: number } {
    return {
        width: getScreenWidth(),
        height: getScreenHeight()
    };
}

/**
 * Gets the device pixel ratio
 */
export function getPixelRatio(): number {
    if (typeof window === 'undefined') return 1;
    return window.devicePixelRatio || 1;
}

/**
 * Checks if the viewport is in portrait orientation
 */
export function isPortrait(): boolean {
    return getViewportHeight() > getViewportWidth();
}

/**
 * Checks if the viewport is in landscape orientation
 */
export function isLandscape(): boolean {
    return getViewportWidth() > getViewportHeight();
}

/**
 * Gets the screen orientation
 */
export function getOrientation(): 'portrait' | 'landscape' {
    return isPortrait() ? 'portrait' : 'landscape';
}

/**
 * Checks if the device is mobile-sized (width < 768px)
 */
export function isMobile(): boolean {
    return getViewportWidth() < 768;
}

/**
 * Checks if the device is tablet-sized (768px <= width < 1024px)
 */
export function isTablet(): boolean {
    const width = getViewportWidth();
    return width >= 768 && width < 1024;
}

/**
 * Checks if the device is desktop-sized (width >= 1024px)
 */
export function isDesktop(): boolean {
    return getViewportWidth() >= 1024;
}

/**
 * Gets the device type based on viewport width
 */
export function getDeviceType(): 'mobile' | 'tablet' | 'desktop' {
    if (isMobile()) return 'mobile';
    if (isTablet()) return 'tablet';
    return 'desktop';
}

/**
 * Checks if the page is being viewed in fullscreen mode
 */
export function isFullscreen(): boolean {
    if (typeof document === 'undefined') return false;
    return !!(
        document.fullscreenElement ||
        (document as any).webkitFullscreenElement ||
        (document as any).mozFullScreenElement ||
        (document as any).msFullscreenElement
    );
}

/**
 * Requests fullscreen mode for the document
 */
export async function requestFullscreen(): Promise<boolean> {
    if (typeof document === 'undefined') return false;
    
    try {
        const elem = document.documentElement;
        if (elem.requestFullscreen) {
            await elem.requestFullscreen();
        } else if ((elem as any).webkitRequestFullscreen) {
            await (elem as any).webkitRequestFullscreen();
        } else if ((elem as any).mozRequestFullScreen) {
            await (elem as any).mozRequestFullScreen();
        } else if ((elem as any).msRequestFullscreen) {
            await (elem as any).msRequestFullscreen();
        }
        return true;
    } catch (e) {
        console.error('Error requesting fullscreen:', e);
        return false;
    }
}

/**
 * Exits fullscreen mode
 */
export async function exitFullscreen(): Promise<boolean> {
    if (typeof document === 'undefined') return false;
    
    try {
        if (document.exitFullscreen) {
            await document.exitFullscreen();
        } else if ((document as any).webkitExitFullscreen) {
            await (document as any).webkitExitFullscreen();
        } else if ((document as any).mozCancelFullScreen) {
            await (document as any).mozCancelFullScreen();
        } else if ((document as any).msExitFullscreen) {
            await (document as any).msExitFullscreen();
        }
        return true;
    } catch (e) {
        console.error('Error exiting fullscreen:', e);
        return false;
    }
}

// Initialize the viewport helpers for direct access from C#
if (typeof window !== 'undefined') {
    (window as any).blazouterViewport = {
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