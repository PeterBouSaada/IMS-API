import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  subject: BehaviorSubject<string> = new BehaviorSubject<string>("");

  constructor() { }

  setSearch(text: string)
  {
    this.subject.next(text);
  }

}
