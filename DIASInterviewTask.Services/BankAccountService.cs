using DAISInterviewTask.Data.Context;
using DAISInterviewTask.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIASInterviewTask.Data.Services
{
    public class BankAccountService
    {
        private readonly DAISInterviewTaskDbContext context;
        private readonly UserManager<User> userManager;

        public BankAccountService(DAISInterviewTaskDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public List<BankAccount> GetUserBankAccounts(string userId)
        {
            if (this.userManager.Users.Any(x => x.Id == userId))
            {
                var accs = this.context.BankAccounts.Where(x => x.UserId == userId && x.IsDeleted == false).ToList();

                return accs;
            }
            else
            {
                throw new ArgumentException("Ivalid user id!");
            }
        }

        public BankAccount GetBankAccountById(string bankAccountId)
        {
            var bankAcc = this.context.BankAccounts.FirstOrDefault(x => x.BankAccountId == bankAccountId);
            return bankAcc;
        }
    }
}
