// Theme management for Blazouter Web Sample
let themeObserver = null;

export function initializeTheme() {
    const theme = localStorage.getItem('theme') || 'light';
    applyTheme(theme === 'dark');

    // Set up observer to prevent theme class from being removed during navigation
    if (!themeObserver) {
        setupThemeProtection();
    }

    return theme === 'dark';
}

function applyTheme(isDark) {
    if (isDark) {
        document.documentElement.classList.add('dark');
    } else {
        document.documentElement.classList.remove('dark');
    }
}

function setupThemeProtection() {
    // Watch for changes to the HTML element's class list
    themeObserver = new MutationObserver(() => {
        const currentTheme = localStorage.getItem('theme') || 'light';
        const shouldBeDark = currentTheme === 'dark';
        const isDark = document.documentElement.classList.contains('dark');

        // If theme class doesn't match what it should be, fix it
        if (shouldBeDark && !isDark) {
            console.log('Theme protection: Re-applying dark mode');
            document.documentElement.classList.add('dark');
        } else if (!shouldBeDark && isDark) {
            console.log('Theme protection: Re-applying light mode');
            document.documentElement.classList.remove('dark');
        }
    });

    // Observe changes to the class attribute
    themeObserver.observe(document.documentElement, {
        attributes: true,
        attributeFilter: ['class']
    });
}

export function isDarkMode() {
    return document.documentElement.classList.contains('dark');
}

export function setDarkMode(isDark) {
    if (isDark) {
        document.documentElement.classList.add('dark');
        localStorage.setItem('theme', 'dark');
    } else {
        document.documentElement.classList.remove('dark');
        localStorage.setItem('theme', 'light');
    }
}

export function toggleTheme() {
    const isDark = !document.documentElement.classList.contains('dark');
    setDarkMode(isDark);
    return isDark;
}

// Ensure theme is applied immediately when module loads
(function () {
    const theme = localStorage.getItem('theme') || 'light';
    if (theme === 'dark') {
        document.documentElement.classList.add('dark');
    }
})();