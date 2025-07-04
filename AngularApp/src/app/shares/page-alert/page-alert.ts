
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'page-alert',
  imports: [],
  templateUrl: './page-alert.html'

})
export class PageAlert implements OnChanges  {
  ngOnChanges(changes: SimpleChanges): void {   
    if(this.message!=""){
      setTimeout(() => {
        this.message="";
      }, 4000);
    }
  }
 
  @Input() message:string ="";
  @Input() alertType:string="";
}
