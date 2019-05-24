import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertService } from 'src/app/_services/alert.service';
import { MarketService } from 'src/app/_services/market.service';
import { Buy } from 'src/app/_models/buy.model';

@Component({
  selector: 'app-buy',
  templateUrl: './buy.component.html',
  styleUrls: ['./buy.component.css']
})
export class BuyComponent implements OnInit {
  buyForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private marketService: MarketService,
    private alertService: AlertService) { }

  ngOnInit() {
    this.buyForm = this.formBuilder.group({
      account: ['', Validators.required],
      amount: ['', Validators.required],
      description: ['', Validators.required],
      reference: ['', Validators.required],
      merchant: ['', Validators.required]
    });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // convenience getter for easy access to form fields
  get f() { return this.buyForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.buyForm.invalid) {
      return;
    }

    this.loading = true;

    this.marketService.buy(this.f.account.value, this.f.amount.value, this.f.description.value, this.f.reference.value, this.f.merchant.value)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }
}
