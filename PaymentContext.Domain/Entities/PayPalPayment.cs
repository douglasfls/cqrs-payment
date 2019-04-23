using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities {
    public class PayPalPayment : Payment {
        public PayPalPayment (DateTime paiedDate, DateTime expireDate, decimal total, decimal totalPayed, Address address, string payer, Document document, Email email, string transactionCode) : base (paiedDate, expireDate, total, totalPayed, address, payer, document, email) {
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; private set; }
    }
}