using DAISInterviewTask.Data.Context;
using DAISInterviewTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIASInterviewTask.Data.Services
{
    public class PaymentService
    {
        private readonly DAISInterviewTaskDbContext context;

        public PaymentService(DAISInterviewTaskDbContext context)
        {
            this.context = context;
        }

        public void CreatePayment(Payment payment)
        {

        }

    }
}
