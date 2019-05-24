import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { MarketService } from '../_services/market.service';
import { AlertService } from '../_services/alert.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  paymentResponse: any;
  chargeAmount = 50.00;
  constructor(private router: Router, private marketService: MarketService, private alertService: AlertService) { }

  ngOnInit() {
  }

  onDropinLoaded(event) {
    console.log("dropin loaded...");
  }

  onPaymentStatus(response): void {
    console.log(response);
    //this.paymentResponse = response;
    var temp = localStorage.getItem('currentUser');
    this.marketService.loadAccount(temp, this.chargeAmount)
      .pipe(first())
      .subscribe(
        data => {
          console.log(data);
        },
        error => {
          this.alertService.error(error);
          console.log(error);
        });
  }
}
