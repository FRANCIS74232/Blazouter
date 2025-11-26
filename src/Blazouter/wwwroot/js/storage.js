/**
 * Storage utilities for Blazouter.
 * Provides type-safe access to localStorage and sessionStorage.
 */
/**
 * Sets an item in localStorage
 * @param key - The storage key
 * @param value - The value to store (will be JSON stringified)
 */
export function setLocalStorage(key, value) {
    if (typeof localStorage === 'undefined')
        return false;
    try {
        localStorage.setItem(key, JSON.stringify(value));
        return true;
    }
    catch (e) {
        console.error('Error setting localStorage:', e);
        return false;
    }
}
/**
 * Gets an item from localStorage
 * @param key - The storage key
 */
export function getLocalStorage(key) {
    if (typeof localStorage === 'undefined')
        return null;
    try {
        const value = localStorage.getItem(key);
        return value ? JSON.parse(value) : null;
    }
    catch (e) {
        console.error('Error getting localStorage:', e);
        return null;
    }
}
/**
 * Removes an item from localStorage
 * @param key - The storage key
 */
export function removeLocalStorage(key) {
    if (typeof localStorage === 'undefined')
        return false;
    try {
        localStorage.removeItem(key);
        return true;
    }
    catch (e) {
        console.error('Error removing localStorage:', e);
        return false;
    }
}
/**
 * Clears all items from localStorage
 */
export function clearLocalStorage() {
    if (typeof localStorage === 'undefined')
        return false;
    try {
        localStorage.clear();
        return true;
    }
    catch (e) {
        console.error('Error clearing localStorage:', e);
        return false;
    }
}
/**
 * Gets all keys from localStorage
 */
export function getLocalStorageKeys() {
    if (typeof localStorage === 'undefined')
        return [];
    const keys = [];
    for (let i = 0; i < localStorage.length; i++) {
        const key = localStorage.key(i);
        if (key)
            keys.push(key);
    }
    return keys;
}
/**
 * Checks if a key exists in localStorage
 * @param key - The storage key
 */
export function hasLocalStorage(key) {
    if (typeof localStorage === 'undefined')
        return false;
    return localStorage.getItem(key) !== null;
}
/**
 * Sets an item in sessionStorage
 * @param key - The storage key
 * @param value - The value to store (will be JSON stringified)
 */
export function setSessionStorage(key, value) {
    if (typeof sessionStorage === 'undefined')
        return false;
    try {
        sessionStorage.setItem(key, JSON.stringify(value));
        return true;
    }
    catch (e) {
        console.error('Error setting sessionStorage:', e);
        return false;
    }
}
/**
 * Gets an item from sessionStorage
 * @param key - The storage key
 */
export function getSessionStorage(key) {
    if (typeof sessionStorage === 'undefined')
        return null;
    try {
        const value = sessionStorage.getItem(key);
        return value ? JSON.parse(value) : null;
    }
    catch (e) {
        console.error('Error getting sessionStorage:', e);
        return null;
    }
}
/**
 * Removes an item from sessionStorage
 * @param key - The storage key
 */
export function removeSessionStorage(key) {
    if (typeof sessionStorage === 'undefined')
        return false;
    try {
        sessionStorage.removeItem(key);
        return true;
    }
    catch (e) {
        console.error('Error removing sessionStorage:', e);
        return false;
    }
}
/**
 * Clears all items from sessionStorage
 */
export function clearSessionStorage() {
    if (typeof sessionStorage === 'undefined')
        return false;
    try {
        sessionStorage.clear();
        return true;
    }
    catch (e) {
        console.error('Error clearing sessionStorage:', e);
        return false;
    }
}
/**
 * Gets all keys from sessionStorage
 */
export function getSessionStorageKeys() {
    if (typeof sessionStorage === 'undefined')
        return [];
    const keys = [];
    for (let i = 0; i < sessionStorage.length; i++) {
        const key = sessionStorage.key(i);
        if (key)
            keys.push(key);
    }
    return keys;
}
/**
 * Checks if a key exists in sessionStorage
 * @param key - The storage key
 */
export function hasSessionStorage(key) {
    if (typeof sessionStorage === 'undefined')
        return false;
    return sessionStorage.getItem(key) !== null;
}
// Initialize the storage helpers for direct access from C#
if (typeof window !== 'undefined') {
    window.blazouterStorage = {
        setLocalStorage,
        getLocalStorage,
        removeLocalStorage,
        clearLocalStorage,
        getLocalStorageKeys,
        hasLocalStorage,
        setSessionStorage,
        getSessionStorage,
        removeSessionStorage,
        clearSessionStorage,
        getSessionStorageKeys,
        hasSessionStorage
    };
}
