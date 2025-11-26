/**
 * Blazouter JavaScript Interop
 *
 * This module provides TypeScript-based JavaScript interop functionality for Blazouter.
 * It includes utilities for browser navigation, document manipulation, and more.
 *
 * @packageDocumentation
 */
export * from './navigation.js';
export * from './document.js';
export * from './storage.js';
export * from './viewport.js';
export * from './clipboard.js';
/**
 * Main Blazouter interop namespace
 */
export declare const Blazouter: {
    version: string;
    /**
     * Initializes Blazouter JavaScript interop
     */
    initialize(): void;
};
