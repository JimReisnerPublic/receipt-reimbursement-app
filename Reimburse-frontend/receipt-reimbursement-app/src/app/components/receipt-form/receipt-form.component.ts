import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ReceiptService } from '../../services/receipt.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-receipt-form',
  templateUrl: './receipt-form.component.html',
  styleUrls: ['./receipt-form.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule]
})
export class ReceiptFormComponent {
  receiptForm: FormGroup;
  categories: string[] = ['Travel', 'Meals', 'Office Supplies', 'Equipment', 'Other'];
  
  constructor(
    private fb: FormBuilder,
    private receiptService: ReceiptService,
    private router: Router
  ) {
    this.receiptForm = this.fb.group({
      employeeId: [1, Validators.required], // Default to logged-in user in a real app
      date: [new Date(), Validators.required],
      amount: ['', [Validators.required, Validators.min(0.01)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      category: ['', Validators.required],
      imageUrl: ['']
    });
  }

  onSubmit(): void {
    if (this.receiptForm.valid) {
      const receipt = {
        ...this.receiptForm.value,
        status: 'Pending',
        submissionDate: new Date()
      };

      this.receiptService.submitReceipt(receipt).subscribe({
        next: () => {
          alert('Receipt submitted successfully!');
          this.router.navigate(['/receipts']);
        },
        error: (err) => {
          console.error('Error submitting receipt', err);
          alert('Failed to submit receipt');
        }
      });
    }
  }
}
