using System.Linq;
using DAISInterviewTask.Data.Context;
using DAISInterviewTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAISInterviewTask.Services;
using Microsoft.AspNetCore.Identity;
using DAISInterviewTask.Data.Models;
using System;
using DAISInterviewTask.Services.Contracts;

namespace DAISInterviewTask.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private DAISInterviewTaskDbContext context;
        private IBankAccountService bankAccountService;
        private readonly IPaymentService paymentService;
        private readonly UserManager<User> userManager;

        public PaymentController(DAISInterviewTaskDbContext context, IBankAccountService bankAccountService,
                                 IPaymentService paymentService, UserManager<User> userManager)
        {
            this.context = context;
            this.bankAccountService = bankAccountService;
            this.paymentService = paymentService;
            this.userManager = userManager;

        }

        public IActionResult Index()
        {
            var accounts = this.context.BankAccounts.Where(x => x.IsDeleted == false).ToList();

            var paymentViewModel = new PaymentViewModel()
            {
                BankAccounts = accounts.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.AccountNumber + " - " + x.Balance + "$"
                            ,
                        Value = x.BankAccountId
                    })
                .ToList()
            };

            return View(paymentViewModel);
        }

        [HttpPost]
        public IActionResult Payment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fromBankAccBalance = this.bankAccountService.GetBankAccountBalanceById(model.FromBankAccountId);
                var fromBankAccountNumber = this.bankAccountService.GetBankAccountNumberById(model.FromBankAccountId);

                if (model.Amount > fromBankAccBalance)
                {
                    ViewData["Error"] = "Invalid payment amount! Payment amount is bigger than bank account balance!";
                    var paymentVM = InitializePaymentViewModel();
                    return View("Index", paymentVM);
                }
                else
                {
                    var userId = this.userManager.GetUserId(HttpContext.User);
                    var result = this.paymentService.CreatePayment(userId, model.FromBankAccountId, model.ToBankAccount, model.Amount, model.Reason);
                    if (result != null)
                    {// everything is fine
                        this.paymentService.ConfirmSuccessfullPayment(result.PaymentId, true);
                        ViewData["Message"] = "Payment is successfull!";
                        ModelState.Clear();
                    }
                    else
                    {// something is not fine
                        this.paymentService.ConfirmSuccessfullPayment(result.PaymentId, false);
                        ViewData["Error"] = "Payment rejected!";
                    }
                }
            }

            var paymentViewModel = InitializePaymentViewModel();


            return View("Index", paymentViewModel);
        }

        public IActionResult ListPayment()
        {
            var userId = this.userManager.GetUserId(HttpContext.User);

            var list = this.context.Payments.Where(x => x.UserId == userId && x.IsDeleted == false).Select(p => new ListPaymentViewModel()
            {
                Amount = p.Amount,
                FromBankAccountId = p.FromBankAccountId,
                Reason = p.Reason,
                ToBankAccount = p.ToBankAccountNumber,
                Status = p.Status,
                CreatedOn = p.CreatedOn
            }).ToList();

            return View(list);
        }

        private PaymentViewModel InitializePaymentViewModel()
        {
            var accounts = this.context.BankAccounts.Where(x => x.IsDeleted == false).ToList();

            var paymentViewModel = new PaymentViewModel()
            {
                BankAccounts = accounts.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.AccountNumber + " - " + x.Balance + "$"
                            ,
                        Value = x.BankAccountId
                    })
                .ToList()
            };

            return paymentViewModel;
        }
    }
}