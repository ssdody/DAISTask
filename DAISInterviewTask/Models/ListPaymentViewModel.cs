using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAISInterviewTask.Models
{
    public class ListPaymentViewModel
    {
        public string ToBankAccount { get; set; }

        public string FromBankAccountId { get; set; }

        public decimal Amount { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
