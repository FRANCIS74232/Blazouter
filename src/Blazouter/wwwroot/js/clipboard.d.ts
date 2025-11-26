/**
 * Clipboard utilities for Blazouter.
 * Provides type-safe access to clipboard operations.
 */
/**
 * Copies text to the clipboard
 * @param text - The text to copy
 */
export declare function copyText(text: string): Promise<boolean>;
/**
 * Reads text from the clipboard
 */
export declare function readText(): Promise<string | null>;
/**
 * Checks if the Clipboard API is supported
 */
export declare function isClipboardSupported(): boolean;
/**
 * Checks if clipboard read permission is granted
 */
export declare function hasClipboardReadPermission(): Promise<boolean>;
/**
 * Checks if clipboard write permission is granted
 */
export declare function hasClipboardWritePermission(): Promise<boolean>;
