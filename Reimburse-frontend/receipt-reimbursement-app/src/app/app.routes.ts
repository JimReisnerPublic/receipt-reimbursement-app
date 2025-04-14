import { Routes } from '@angular/router';
import { ReceiptFormComponent } from './components/receipt-form/receipt-form.component';

export const routes: Routes = [
  { path: 'receipt-form', component: ReceiptFormComponent },
  //  default route
  { path: '', redirectTo: '/receipt-form', pathMatch: 'full' }
];
