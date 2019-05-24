import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @Input() balance: number;
  @Output() open: EventEmitter<any> = new EventEmitter();
  constructor() { }

  ngOnInit() {
    this.open.emit(this.balance);
  }
}
