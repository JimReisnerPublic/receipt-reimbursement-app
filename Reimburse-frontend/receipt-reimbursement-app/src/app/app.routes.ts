import { Routes } from '@angular/router';
import { ReceiptFormComponent } from './components/receipt-form/receipt-form.component';
import { ReceiptListComponent } from './components/receipt-list/receipt-list.component';

export const routes: Routes = [
  { path: 'receipt-form', component: ReceiptFormComponent },
  { path: 'receipts', component: ReceiptListComponent }, // Add this line
  { path: '', redirectTo: '/receipt-form', pathMatch: 'full' }
];
