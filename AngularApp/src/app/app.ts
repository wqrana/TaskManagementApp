import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AppTaskList } from "./pages/app-task-list/app-task-list";

@Component({
  selector: 'app-root',
  imports: [AppTaskList],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'AngularApp';
}
