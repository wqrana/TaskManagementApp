
import { Component, signal, Signal } from '@angular/core';
import { TaskService } from '../../services/taskService';
import { Task } from '../../models/task';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import {DateFormatPipe} from '../../pipes/date-format-pipe'
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { ViewportScroller } from '@angular/common';
@Component({
  selector: 'app-task-list',  
  templateUrl: './app-task-list.html',
  styleUrl: './app-task-list.css',
  imports: [FormsModule, CommonModule,MatDatepickerModule,MatInputModule,MatNativeDateModule]
  
})
export class AppTaskList {
tasks: Task[] = [];
newTask: Task = this.initTask();
actionType = signal<string>("Add"); 

  constructor(private taskService: TaskService,private viewportScroller: ViewportScroller) {}
 
  ngOnInit(): void {
  
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getTasks().subscribe(tasks => {
      this.tasks = tasks;
    });
  }
  saveTask():void{
    if(this.actionType()=="Add"){
      this.taskService.addTask(this.newTask).subscribe(() => {
      this.loadTasks();
      this.resetTask();
    });
    }
    else{
      this.taskService.updateTask(this.newTask).subscribe(() => {
      this.loadTasks();
      this.resetTask();
    });
  }
}

editTask(id:number):void{
  this.taskService.getTask(id).subscribe((task) => {
  this.actionType.set("Edit");
   this.scrollToTop();
  this.newTask = task;  
      
  });

}

  toggleStatus(task: Task): void {
    // Using jQuery for the toggle effect
    // $(`#task-${task.id}`).fadeOut(200, () => {
    //   task.isCompleted = !task.isCompleted;
    //   this.taskService.updateTask(task).subscribe(() => {
    //     $(`#task-${task.id}`).fadeIn(200);
    //   });
    // });
  }

  deleteTask(id: number): void {
    if (confirm('Are you sure you want to delete this task?')) {
      this.taskService.deleteTask(id).subscribe(() => {
        this.loadTasks();
      });
    }
  }
  resetTask(){
    this.newTask = this.initTask();
    this.actionType.set("Add");
  }
 initTask():Task{
   return this.newTask = {
      id: 0,
      title: '',
      description: '',
      priority: 'Medium',
      dueDate: new Date(),
      isCompleted: false,
      createdAt: new Date()   
      
    };
  }
   scrollToTop() {
    this.viewportScroller.scrollToPosition([0, 0]);
  }
}
