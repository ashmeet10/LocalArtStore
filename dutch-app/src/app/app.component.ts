import { Component } from '@angular/core';

@Component({
  selector: 'my-app',
  template: `
    <div style="text-align:center">
      <h1>
        Welcome to {{title}}!
      </h1>
    </div>
    
`,
  styles: []
})
export class AppComponent {
  title = 'Dutch Treat';
}
