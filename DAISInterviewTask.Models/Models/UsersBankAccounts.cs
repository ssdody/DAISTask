namespace DAISInterviewTask.Data.Models
{
    public class UsersBankAccounts
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }
    }
}
