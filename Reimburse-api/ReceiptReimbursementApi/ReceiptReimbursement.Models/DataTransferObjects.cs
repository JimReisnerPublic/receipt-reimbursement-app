using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptReimbursement.Models
{
    public class ReceiptEmailSubmission
    {
        public string EmployeeEmail { get; set; }
        public Receipt Receipt { get; set; }
    }

}
