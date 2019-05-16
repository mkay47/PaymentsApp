import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AccountComponent } from './account/account.component';
import { BuyComponent } from './market/buy/buy.component';
import { SellComponent } from './market/sell/sell.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch:'full' },
  { path: 'login', component: LoginComponent },
  { path: 'account', component: AccountComponent },
  { path: 'buy', component: BuyComponent },
  { path: 'sell', component: SellComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
