using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public sealed class CreditCardPayment : Payment {
        public CreditCardPayment (DateTime paiedDate, DateTime expireDate, decimal total, decimal totalPayed, Address address, string payer, Document document, Email email, string cardHolderName, string cardNumber, string lastTransactionNumber) : base (paiedDate, expireDate, total, totalPayed, address, payer, document, email) {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }
}