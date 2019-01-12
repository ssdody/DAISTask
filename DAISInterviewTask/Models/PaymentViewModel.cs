using DAISInterviewTask.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAISInterviewTask.Models
{
    public class PaymentViewModel
    {
        [Display(Name = "Bank Accounts")]
        public List<SelectListItem> BankAccounts { get; set; }

        [Display(Name = "Payment to Bank Account")]
        [StringLength(22, MinimumLength = 22)]
        [RegularExpression("^[A-Za-z0-9]{22}$", ErrorMessage = "Account number must be exacly 22 symbols!")]
        public string ToBankAccount { get; set; }

        public string FromBankAccountId { get; set; }

        [Display(Name = "Amount")]
        [Range(1, 9999999999999999.99)]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "The amount must be positive integer with only two digits after decimal point.")]
        public decimal Amount { get; set; }

        [MaxLength(32)]
        [Display(Name = "Reason")]
        public string Reason { get; set; }
    }
}
