using DAISInterviewTask.Data.Context;
using DAISInterviewTask.Data.Models;
using DAISInterviewTask.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAISInterviewTask.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly DAISInterviewTaskDbContext context;
        private readonly IBankAccountService bankAccountService;
        private readonly UserManager<User> userManager;

        public PaymentService(DAISInterviewTaskDbContext context, IBankAccountService bankAccountService, UserManager<User> userManager)
        {
            this.context = context;
            this.bankAccountService = bankAccountService;
            this.userManager = userManager;
        }

        public Payment CreatePayment(string userId, string fromBankAccountId, string toBankAccountNumber, decimal amount, string reason)
        {

            var payment = new Payment()
            {
                FromBankAccountId = fromBankAccountId,
                ToBankAccountNumber = toBankAccountNumber,
                Reason = reason,
                Amount = amount,
                UserId = userId,
                Status = "Waiting",
                CreatedOn = DateTime.Now
            };

            this.context.Payments.Add(payment);
            this.context.SaveChanges();

            return payment;
        }

        public Payment ConfirmSuccessfullPayment(string paymentId, bool isSuccessfull)
        {
            var waitingPayment = this.context.Payments.FirstOrDefault(x => x.PaymentId == paymentId);

            if (waitingPayment != null)
            {
                var fromBankAccount = this.context.BankAccounts.FirstOrDefault(x => x.BankAccountId == waitingPayment.FromBankAccountId);
                if (isSuccessfull && fromBankAccount.Balance >= waitingPayment.Amount)
                {
                    waitingPayment.Status = "Successfull";
                    var amount = waitingPayment.Amount;

                    fromBankAccount.Balance -= amount;
                }
                else
                {
                    waitingPayment.Status = "Rejected";
                }
            }

            this.context.Payments.Update(waitingPayment);
            this.context.SaveChanges();

            return waitingPayment;
        }

        public List<Payment> GetUserPaymentsById(string userId)
        {
            var list = this.context.Payments.Where(x => x.UserId == userId && x.IsDeleted == false).ToList();

            return list;
        }

    }
}
