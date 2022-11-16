using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities {
    public sealed class BoletoPayment : Payment {
        public BoletoPayment (DateTime paiedDate, DateTime expireDate, decimal total, decimal totalPayed, Address address, string payer, Document document, Email email, string barCode, string boletoNumber) : base (paiedDate, expireDate, total, totalPayed, address, payer, document, email) {
            BarCode = barCode;
            BoletoNumber = boletoNumber;
        }
        public string BarCode { get; private set; }
        public string BoletoNumber { get; private set; }
    }
}