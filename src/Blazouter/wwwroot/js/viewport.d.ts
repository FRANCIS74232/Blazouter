/**
 * Viewport utilities for Blazouter.
 * Provides information about the browser viewport and window dimensions.
 */
/**
 * Gets the current viewport width
 */
export declare function getViewportWidth(): number;
/**
 * Gets the current viewport height
 */
export declare function getViewportHeight(): number;
/**
 * Gets the current viewport dimensions
 */
export declare function getViewportSize(): {
    width: number;
    height: number;
};
/**
 * Gets the screen width
 */
export declare function getScreenWidth(): number;
/**
 * Gets the screen height
 */
export declare function getScreenHeight(): number;
/**
 * Gets the screen dimensions
 */
export declare function getScreenSize(): {
    width: number;
    height: number;
};
/**
 * Gets the device pixel ratio
 */
export declare function getPixelRatio(): number;
/**
 * Checks if the viewport is in portrait orientation
 */
export declare function isPortrait(): boolean;
/**
 * Checks if the viewport is in landscape orientation
 */
export declare function isLandscape(): boolean;
/**
 * Gets the screen orientation
 */
export declare function getOrientation(): 'portrait' | 'landscape';
/**
 * Checks if the device is mobile-sized (width < 768px)
 */
export declare function isMobile(): boolean;
/**
 * Checks if the device is tablet-sized (768px <= width < 1024px)
 */
export declare function isTablet(): boolean;
/**
 * Checks if the device is desktop-sized (width >= 1024px)
 */
export declare function isDesktop(): boolean;
/**
 * Gets the device type based on viewport width
 */
export declare function getDeviceType(): 'mobile' | 'tablet' | 'desktop';
/**
 * Checks if the page is being viewed in fullscreen mode
 */
export declare function isFullscreen(): boolean;
/**
 * Requests fullscreen mode for the document
 */
export declare function requestFullscreen(): Promise<boolean>;
/**
 * Exits fullscreen mode
 */
export declare function exitFullscreen(): Promise<boolean>;
