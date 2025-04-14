import { Component } from '@angular/core';
import { ReceiptService } from '../../services/receipt.service';
import { Receipt } from '../../models/receipt.model';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-receipt-list',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './receipt-list.component.html',
  styleUrl: './receipt-list.component.css'
})
export class ReceiptListComponent {
  receipts: Receipt[] = [];

  constructor(private receiptService: ReceiptService) {}

  ngOnInit() {
    this.receiptService.getReceipts().subscribe(receipts => {
      this.receipts = receipts;
    });
  }
}
