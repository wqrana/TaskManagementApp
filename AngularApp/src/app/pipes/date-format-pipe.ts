import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateFormat'
})
export class DateFormatPipe implements PipeTransform {

 transform(date: Date): string {
    return date?.toISOString().split('T')[0] || '';;
  }

}
