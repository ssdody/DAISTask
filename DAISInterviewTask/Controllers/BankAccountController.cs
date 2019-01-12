using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAISInterviewTask.Data.Context;
using DAISInterviewTask.Data.Models;
using DAISInterviewTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DAISInterviewTask.Controllers
{
    public class BankAccountController : Controller
    {
        private readonly DAISInterviewTaskDbContext context;
        private readonly UserManager<User> userManager;

        public BankAccountController(DAISInterviewTaskDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(HttpContext.User);
            var accounts = this.context.BankAccounts.Where(x => x.UserId == userId).ToList();

            var list = accounts.Select(x => new BankAccountViewModel() { AccountNumber = x.AccountNumber, Balance = x.Balance }).ToList();
           
            return View(list);
        }
    }
}