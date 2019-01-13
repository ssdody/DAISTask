using DAISInterviewTask.Data.Models;
using System.Collections.Generic;

namespace DAISInterviewTask.Services.Contracts
{
    public interface IPaymentService
    {
        Payment CreatePayment(string userId, string fromBankAccountId, string toBankAccountNumber, decimal amount, string reason);
        Payment ConfirmSuccessfullPayment(string paymentId, bool isSuccessfull);
        List<Payment> GetUserPaymentsById(string userId);
    }
}