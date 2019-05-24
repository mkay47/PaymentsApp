import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-market',
  templateUrl: './market.component.html',
  styleUrls: ['./market.component.css']
})
export class MarketComponent implements OnInit {
  constructor(private router: Router) { }

  ngOnInit() {
  }

  goSell() {
    this.router.navigate(['/sell']);
  }

  goBuy() {
    this.router.navigate(['/buy']);
  }

  goAccount() {
    this.router.navigate(['/account']);
  }
}
