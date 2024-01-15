module.exports = {
    printWidth: 80,
    tabWidth: 2,
    singleQuote: false,
    semi: true,
    trailingComma: "all",
    bracketSpacing: true,
    bracketSameLine: false,
    arrowParens: "always",
    importOrder: [
      "^react(.*)",
      "^@react(.*)",
      "^[^//]+$",
      "^@mui/(.*)",
      "<THIRD_PARTY_MODULES>",
      "@/(.*)",
      "^./(.*)",
    ],
    importOrderSeparation: true,
    overrides: [
      {
        files: "*.{js,jsx,tsx,ts,scss,json,html}",
        options: {
          tabWidth: 2,
        },
      },
    ],
  };
  