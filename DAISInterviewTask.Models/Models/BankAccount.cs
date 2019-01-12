using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAISInterviewTask.Data.Models
{
    public class BankAccount
    {
        [Key]
        public string BankAccountId { get; set; }

        public string UserId { get; set; }
        [Required]
        public User User { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        [StringLength(22, MinimumLength = 22)]
        public string AccountNumber { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount")]
        [Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        //public ICollection<UsersBankAccounts> UsersBankAccounts { get; set; }

        public bool IsDeleted { get; set; }
        
    }
}
