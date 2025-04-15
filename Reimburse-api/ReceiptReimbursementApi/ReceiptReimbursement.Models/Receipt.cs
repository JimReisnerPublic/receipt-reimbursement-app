namespace ReceiptReimbursement.Models;

public class Receipt
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Status { get; set; }
    public string ImageLocation { get; set; } 
    public DateTime SubmissionDate { get; set; }
}
