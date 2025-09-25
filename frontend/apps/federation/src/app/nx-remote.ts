// src/app/button-wrapper/button-wrapper.component.ts

import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { loadRemoteModule } from '@angular-architects/module-federation';
import * as React from 'react';
import * as ReactDOM from 'react-dom/client';

@Component({
  selector: 'nx-remote-component',
  template: '<div #remoteContainer></div>',
  styleUrls: [],
})
export class NxRemoteComponent implements AfterViewInit {
  @ViewChild('remoteContainer', { static: true }) remoteContainer!: ElementRef;

  async ngAfterViewInit() {
    const ButtonModule = await loadRemoteModule({
      remoteEntry: 'http://localhost:4200/remoteEntry.js',
      remoteName: 'backoffice_core',
      exposedModule: './Routes',
    });

    const ReactComponent = ButtonModule.default; // Assuming default export
    console.log(ReactComponent);
    const reactElement = React.createElement(ReactComponent, {});

    const container = this.remoteContainer.nativeElement;
    const root = ReactDOM.createRoot(container);
    root.render(reactElement); // Render using 'createRoot'
  }
}
