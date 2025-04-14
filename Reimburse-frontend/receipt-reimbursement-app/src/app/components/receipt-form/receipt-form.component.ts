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
  selectedFile: File | null = null;
  categories: string[] = ['Travel', 'Meals', 'Office Supplies', 'Equipment', 'Other'];
  infoMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private receiptService: ReceiptService,
    private router: Router
  ) {
    this.receiptForm = this.fb.group({
      employeeEmail: ['', [Validators.required, Validators.email]],
      date: [new Date().toISOString().substring(0, 10), Validators.required], // Store as yyyy-MM-dd
      amount: ['', [Validators.required, Validators.min(0.01)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      category: ['', Validators.required],
      receiptFile: [null, [Validators.required]]
    });
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      this.receiptForm.patchValue({ receiptFile: file });
    }
  }

onSubmit(): void {
  this.infoMessage = ''; // Clear previous messages
  if (this.receiptForm.valid && this.selectedFile) {
    const formData = new FormData();
    const { employeeEmail, date, amount, description, category } = this.receiptForm.value;

    const parsedDate = new Date(date);

    formData.append('employeeEmail', employeeEmail);
    formData.append('date', parsedDate.toISOString());
    formData.append('amount', amount);
    formData.append('description', description);
    formData.append('category', category);
    formData.append('receiptFile', this.selectedFile);

    this.receiptService.submitReceiptByEmail(formData).subscribe({
      next: () => {
        alert('Receipt submitted successfully!');
        this.router.navigate(['/receipts']);
      },
      error: (err) => {
        console.error('Error submitting receipt', err);
        // Check for file size error
        if (err.status === 413) {
          this.infoMessage = 'The uploaded file is too large. Maximum allowed size is 5MB.';
        } else if (
          err.status === 400 &&
          err.error?.message &&
          err.error.message.toLowerCase().includes('file size')
        ) {
          this.infoMessage = err.error.message;
        } else {
          const errorMessage = err.error?.message || 'Failed to submit receipt. Please try again later.';
          this.infoMessage = errorMessage;
        }
      }
    });
  }
}

}

