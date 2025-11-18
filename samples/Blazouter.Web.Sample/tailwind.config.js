/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Blazouter.Web.Sample/**/*.{cs,razor,html,cshtml}",
        "./Blazouter.Web.Client.Sample/**/*.{cs,razor,html,cshtml}"
    ],
    safelist: [],
    darkMode: 'class',
    theme: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/forms'),
        require('@tailwindcss/typography'),
    ],
}