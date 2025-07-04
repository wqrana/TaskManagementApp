export interface Task {
    id: number;
    title?: string;
    description?: string;
    priority: 'High' | 'Medium' | 'Low';
    dueDate: Date;
    isCompleted: boolean;
    createdAt: Date;   
   
  }