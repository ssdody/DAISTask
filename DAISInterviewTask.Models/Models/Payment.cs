using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAISInterviewTask.Data.Models
{
    public class Payment
    {
        [Key]
        public string PaymentId { get; set; }

        public string UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string ToBankAccountNumber { get; set; }

        [Required]
        public string FromBankAccountId { get; set; }

        [Required]
        public BankAccount FromBankAccount { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 10)]
        public string Reason { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
