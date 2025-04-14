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

  deleteReceipt(id: number): void {
    if (confirm('Are you sure you want to delete this receipt?')) {
      this.receiptService.deleteReceipt(id).subscribe({
        next: () => {
          // Refresh the list after deletion
          this.receipts = this.receipts.filter(r => r.id !== id);
          alert('Receipt deleted successfully!');
        },
        error: (err) => {
          console.error('Error deleting receipt', err);
          alert('Failed to delete receipt. Please try again.');
        }
      });
    }
  }
}
