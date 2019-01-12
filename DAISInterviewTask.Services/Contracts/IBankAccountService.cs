using System.Collections.Generic;
using DAISInterviewTask.Data.Models;

namespace DAISInterviewTask.Services.Contracts
{
    public interface IBankAccountService
    {
        decimal GetBankAccountBalanceById(string bankAccountId);
        BankAccount GetBankAccountById(string bankAccountId);
        List<BankAccount> GetUserBankAccounts(string userId);
        string GetBankAccountNumberById(string bankAccountId);
    }
}