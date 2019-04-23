using System;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities {
    public abstract class Payment : Entity {

        protected Payment (DateTime paiedDate, DateTime expireDate, decimal total, decimal totalPayed, Address address, string payer, Document document, Email email) {
            Number = Guid.NewGuid ().ToString ().Replace ("-", "").Substring (0, 10).ToUpper ();
            PaiedDate = paiedDate;
            ExpireDate = expireDate;
            Total = total;
            TotalPayed = totalPayed;
            Address = address;
            Payer = payer;
            Document = document;
            Email = email;

            AddNotifications (new Contract ()
                .Requires ()
                .IsGreaterThan (Total, 0, "Payment.Total", "O total não pode ser zero")
                .IsGreaterOrEqualsThan (TotalPayed, Total, "Payment.TotalPayed", "O valor pago deve ser maior ou igual ao total"));
        }

        public string Number { get; private set; }
        public DateTime PaiedDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalPayed { get; private set; }
        public Address Address { get; private set; }
        public string Payer { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
    }
}