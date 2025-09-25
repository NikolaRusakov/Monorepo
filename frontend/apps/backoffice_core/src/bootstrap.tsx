// import { StrictMode } from 'react';
// import * as ReactDOM from 'react-dom/client';
import App from './app/app';
import r2wc from '@r2wc/react-to-web-component';

// const root = ReactDOM.createRoot(
//   document.getElementById('root') as HTMLElement,
// );
// root.render(
//   <StrictMode>
//     <App />
//   </StrictMode>,
// );

// export default App;

// import React from 'react';
// import ReactDOM from 'react-dom';
// class Mfe4Element extends HTMLElement {
//   connectedCallback() {
//     console.log('http-mfe-react-element connectedCallback from DOM');

//     window.React = React;
//     ReactDOM.createRoot(App  );
//   }

//   disconnectedCallback() {
//     console.log('http-mfe-react-element disconnectedCallback from DOM');
//   }
// }

// customElements.define('http-mfe-react-element', Mfe4Element);

// export default Mfe4Element;


export function defineReactWebComponent() {
  // Define the new custom element with the element name for the React Web Component.
  if (!customElements.get('wc-remote3')) {
    // This is where we convert the React component to a Web Component
    customElements.define('http-mfe-react-element', r2wc(App));
  }
}

defineReactWebComponent();
export default App;
