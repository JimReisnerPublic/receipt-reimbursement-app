import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Receipt } from '../models/receipt.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ReceiptService {
  private apiUrl = 'http://localhost:5096/api/receipts';

  constructor(private http: HttpClient) { }

  getReceipts(): Observable<Receipt[]> {
    return this.http.get<Receipt[]>(this.apiUrl);
  }

  getReceiptById(id: number): Observable<Receipt> {
    return this.http.get<Receipt>(`${this.apiUrl}/${id}`);
  }

  submitReceipt(receipt: Receipt): Observable<Receipt> {
    return this.http.post<Receipt>(this.apiUrl, receipt);
  }

  submitReceiptByEmail(formData: FormData): Observable<Receipt> {
    return this.http.post<Receipt>(
      `${this.apiUrl}/by-email`,
      formData,
      {
        headers: new HttpHeaders({
          // Let the browser set the correct Content-Type
        })
      }
    );
  }
  

  updateReceipt(id: number, receipt: Receipt): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, receipt);
  }

  deleteReceipt(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
