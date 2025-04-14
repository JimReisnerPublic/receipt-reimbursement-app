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
      employeeEmail: ['', [Validators.required, Validators.email]],
      date: [new Date(), Validators.required],
      amount: ['', [Validators.required, Validators.min(0.01)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      category: ['', Validators.required],
      imageUrl: ['']
    });
  }

  onSubmit(): void {
    if (this.receiptForm.valid) {
      const { employeeEmail, ...receiptData } = this.receiptForm.value;
      const submission = {
        employeeEmail: employeeEmail,
        receipt: {
          ...receiptData,
          status: 'Pending',
          submissionDate: new Date()
        }
      };

      this.receiptService.submitReceiptByEmail(submission).subscribe({
        next: () => {
          alert('Receipt submitted successfully!');
          this.router.navigate(['/receipts']);
        },
        error: (err) => {
          console.error('Error submitting receipt', err);
          const errorMessage = err.error?.message || 'Failed to submit receipt. Please try again later.';
          
          if (err.status === 404) {
            alert(`Employee not found: ${errorMessage}`);
          } else {
            alert(errorMessage);
          }
        }
      });
    }
  }
}
