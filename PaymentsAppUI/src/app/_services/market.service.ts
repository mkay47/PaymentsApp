import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from "../../environments/environment";

@Injectable()
export class MarketService {
  constructor(private http: HttpClient) { }

  buy(account: string, amount: number, description: string, reference: string, merchant: string) {
    return this.http.post<any>(`${environment.api}/Market/buy`, { account: account, amount: amount, description: description, reference: reference, merchant: merchant })
      .pipe(map(response => {
        console.log(response);

        return response;
      }));
  }

  sell(account: string, amount: number) {
    return this.http.post<any>(`${environment.api}/Market/sell`, { account: account, amount: amount })
      .pipe(map(data => {
        console.log(data);

        return data;
      }));
  }

  loadAccount(account: string, amount: number) {
    return this.http.post<any>(`${environment.api}/Market/account`, { account: account, amount: amount })
      .pipe(map(data => {
        console.log(data);

        return data;
      }));
  }
}
