export interface Receipt {
  id?: number;
  date: Date;
  amount: number;
  description: string;
  category: string;
  status: string;
  imageLocation?: string;  // Changed from imageUrl
  submissionDate: Date;
}

  export interface Employee {
    id: number;
    name: string;
    email: string;
    department: string;
  }
  