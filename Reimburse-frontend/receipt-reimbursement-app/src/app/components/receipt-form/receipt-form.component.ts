import { Component, ViewChild, ElementRef } from '@angular/core';
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
  @ViewChild('fileInput') fileInput!: ElementRef;
  receiptForm: FormGroup;
  selectedFile: File | null = null;
  categories: string[] = ['Travel', 'Meals', 'Office Supplies', 'Equipment', 'Other'];
  infoMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private receiptService: ReceiptService,
    private router: Router
  ) {
    const today = new Date().toISOString().substring(0, 10); // Format as yyyy-MM-dd
    this.receiptForm = this.fb.group({
      employeeEmail: ['', [Validators.required, Validators.email]],
      date: [today, Validators.required], 
      amount: ['', [Validators.required, Validators.min(0.01)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      category: ['', Validators.required],
      receiptFile: [null, [Validators.required]]
    });
  }

onFileSelected(event: any) {
  const file = event.target.files[0];
  if (file) {
    // Clear previous messages
    this.infoMessage = '';
    
    // Check file size (5MB limit)
    if (file.size > 5 * 1024 * 1024) {
      this.infoMessage = 'File size exceeds 5MB limit. Please choose a smaller file.';
      this.selectedFile = null;
      this.receiptForm.patchValue({ receiptFile: null });
      event.target.value = ''; // Clear the file input
      alert(this.infoMessage);
      return;
    }
    
    this.selectedFile = file;
    this.receiptForm.patchValue({ receiptFile: file });
  }
}

onSubmit(): void {
  this.infoMessage = ''; // Clear previous messages
  if (this.receiptForm.valid && this.selectedFile) {
    const formData = new FormData();
    const { employeeEmail, date, amount, description, category } = this.receiptForm.value;

    //const parsedDate = new Date(date);

    formData.append('employeeEmail', employeeEmail);
    //formData.append('date', parsedDate.toISOString());
    formData.append('date', date);
    formData.append('amount', amount);
    formData.append('description', description);
    formData.append('category', category);
    formData.append('receiptFile', this.selectedFile);

    this.receiptService.submitReceiptByEmail(formData).subscribe({
      next: () => {
        alert('Receipt submitted successfully!');
        // Reset the form and clear the file
        this.fileInput.nativeElement.value = '';
        this.receiptForm.reset();
        this.selectedFile = null;
        // Reset the date field to today's date
        const today = new Date().toISOString().substring(0, 10);
        this.receiptForm.patchValue({ date: today });
        
        // Reset validation states
        Object.keys(this.receiptForm.controls).forEach(key => {
          this.receiptForm.get(key)?.setErrors(null);
          this.receiptForm.get(key)?.markAsUntouched();
        });
        // Reset the date field to today's date
       // const today = new Date().toISOString().substring(0, 10);
        //this.receiptForm.patchValue({ date: today });
        this.router.navigate(['/receipt-form']);
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

