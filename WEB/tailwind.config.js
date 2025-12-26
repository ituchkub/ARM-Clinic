/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        'custom-blue': '#B8317D', // Add your custom color here
      },
    },
  },
  daisyui: {
    themes: [
      {
        garden: {
          ...require("daisyui/src/theming/themes")["emerald"],
          primary: "#83cc16",
          warning: "#f6d860",
          accent: "#37cdbe",
          neutral: "#B6B6B6",
          error: "#e42300",
          success: "#03C223",
          info: "#00b3f0", // Optionally replace the info color with your custom blue
        },
      },
    ],
  },
  plugins: [require("daisyui")],
};
