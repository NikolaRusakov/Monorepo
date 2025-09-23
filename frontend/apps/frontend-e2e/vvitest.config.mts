import { defineConfig } from 'vitest/config';

export default defineConfig({
  test: {
    includeSource: ['src/**/*.{js,ts}'],
    reporters: ['html'],
    browser: {
      instances: [
        {
          browser: 'chrome',
          launch: {},
          connect: {},
          context: {},
        },
      ],
    },
  },
});
