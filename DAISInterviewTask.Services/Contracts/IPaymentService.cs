using DAISInterviewTask.Data.Models;

namespace DAISInterviewTask.Services.Contracts
{
    public interface IPaymentService
    {
        Payment CreatePayment(string userId, string fromBankAccountId, string toBankAccountNumber, decimal amount, string reason);
        Payment ConfirmSuccessfullPayment(string paymentId, bool isSuccessfull);
    }
}