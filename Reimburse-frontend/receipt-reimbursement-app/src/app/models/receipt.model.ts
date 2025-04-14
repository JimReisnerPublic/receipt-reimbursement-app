export interface Receipt {
    id?: number;
    employeeId: number;
    date: Date;
    amount: number;
    description: string;
    category: string;
    status: 'Pending' | 'Approved' | 'Rejected';
    imageUrl?: string;
    submissionDate: Date;
  }
  
  export interface Employee {
    id: number;
    name: string;
    email: string;
    department: string;
  }
  