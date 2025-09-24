import { Component, ViewEncapsulation } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NxWelcome } from './nx-welcome';

@Component({
  imports: [NxWelcome, RouterModule],
  selector: 'app-root',
  template: `<app-nx-welcome></app-nx-welcome> <router-outlet></router-outlet>`,
  styles: ``,
  encapsulation: ViewEncapsulation.ShadowDom,
})
export class App {
  protected title = 'host';
}
