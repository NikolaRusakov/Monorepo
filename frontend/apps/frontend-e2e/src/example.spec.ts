// import { test, expect } from '@playwright/test';
import { page } from '@vitest/browser/context';

import { describe, expect, it, test } from 'vitest';

describe('app page', async (/* { page } */) => {
  it('has title', async () => {
    await page.goto('/');

    // Expect h1 to contain a substring.
    expect(await page.locator('h1').innerText()).toContain('Welcome');
  });
});
