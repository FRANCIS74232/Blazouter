/**
 * Storage utilities for Blazouter.
 * Provides type-safe access to localStorage and sessionStorage.
 */
/**
 * Sets an item in localStorage
 * @param key - The storage key
 * @param value - The value to store (will be JSON stringified)
 */
export declare function setLocalStorage(key: string, value: any): boolean;
/**
 * Gets an item from localStorage
 * @param key - The storage key
 */
export declare function getLocalStorage(key: string): any;
/**
 * Removes an item from localStorage
 * @param key - The storage key
 */
export declare function removeLocalStorage(key: string): boolean;
/**
 * Clears all items from localStorage
 */
export declare function clearLocalStorage(): boolean;
/**
 * Gets all keys from localStorage
 */
export declare function getLocalStorageKeys(): string[];
/**
 * Checks if a key exists in localStorage
 * @param key - The storage key
 */
export declare function hasLocalStorage(key: string): boolean;
/**
 * Sets an item in sessionStorage
 * @param key - The storage key
 * @param value - The value to store (will be JSON stringified)
 */
export declare function setSessionStorage(key: string, value: any): boolean;
/**
 * Gets an item from sessionStorage
 * @param key - The storage key
 */
export declare function getSessionStorage(key: string): any;
/**
 * Removes an item from sessionStorage
 * @param key - The storage key
 */
export declare function removeSessionStorage(key: string): boolean;
/**
 * Clears all items from sessionStorage
 */
export declare function clearSessionStorage(): boolean;
/**
 * Gets all keys from sessionStorage
 */
export declare function getSessionStorageKeys(): string[];
/**
 * Checks if a key exists in sessionStorage
 * @param key - The storage key
 */
export declare function hasSessionStorage(key: string): boolean;
