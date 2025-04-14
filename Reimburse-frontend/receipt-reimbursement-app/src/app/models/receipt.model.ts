export interface Receipt {
  id?: number;
  date: Date;
  amount: number;
  description: string;
  category: string;
  status: string;
  imageUrl?: string;
  submissionDate: Date;
}

  export interface Employee {
    id: number;
    name: string;
    email: string;
    department: string;
  }
  