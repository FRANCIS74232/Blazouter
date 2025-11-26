/**
 * Clipboard utilities for Blazouter.
 * Provides type-safe access to clipboard operations.
 */
/**
 * Copies text to the clipboard
 * @param text - The text to copy
 */
export async function copyText(text) {
    if (typeof navigator === 'undefined' || !navigator.clipboard) {
        return fallbackCopyText(text);
    }
    try {
        await navigator.clipboard.writeText(text);
        return true;
    }
    catch (e) {
        console.error('Error copying text to clipboard:', e);
        return fallbackCopyText(text);
    }
}
/**
 * Fallback method for copying text (for older browsers)
 * @param text - The text to copy
 */
function fallbackCopyText(text) {
    if (typeof document === 'undefined')
        return false;
    try {
        const textarea = document.createElement('textarea');
        textarea.value = text;
        textarea.style.position = 'fixed';
        textarea.style.opacity = '0';
        document.body.appendChild(textarea);
        textarea.select();
        const successful = document.execCommand('copy');
        document.body.removeChild(textarea);
        return successful;
    }
    catch (e) {
        console.error('Fallback copy failed:', e);
        return false;
    }
}
/**
 * Reads text from the clipboard
 */
export async function readText() {
    if (typeof navigator === 'undefined' || !navigator.clipboard) {
        console.warn('Clipboard API not available');
        return null;
    }
    try {
        const text = await navigator.clipboard.readText();
        return text;
    }
    catch (e) {
        console.error('Error reading text from clipboard:', e);
        return null;
    }
}
/**
 * Checks if the Clipboard API is supported
 */
export function isClipboardSupported() {
    return typeof navigator !== 'undefined' &&
        navigator.clipboard !== undefined &&
        typeof navigator.clipboard.writeText === 'function';
}
/**
 * Checks if clipboard read permission is granted
 */
export async function hasClipboardReadPermission() {
    if (typeof navigator === 'undefined' || !navigator.permissions) {
        return false;
    }
    try {
        const result = await navigator.permissions.query({ name: 'clipboard-read' });
        return result.state === 'granted';
    }
    catch (e) {
        return false;
    }
}
/**
 * Checks if clipboard write permission is granted
 */
export async function hasClipboardWritePermission() {
    if (typeof navigator === 'undefined' || !navigator.permissions) {
        return false;
    }
    try {
        const result = await navigator.permissions.query({ name: 'clipboard-write' });
        return result.state === 'granted';
    }
    catch (e) {
        return false;
    }
}
// Initialize the clipboard helpers for direct access from C#
if (typeof window !== 'undefined') {
    window.blazouterClipboard = {
        copyText,
        readText,
        isClipboardSupported,
        hasClipboardReadPermission,
        hasClipboardWritePermission
    };
}
