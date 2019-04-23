using Flunt.Validations;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands {
    public class CreateCreditCardSubscriptionCommand : CreateSubscriptionCommand, ICommand {

        public CreateCreditCardSubscriptionCommand () {

        }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string LastTransactionNumber { get; set; }
        public new void Validate () {
            base.Validate ();
            AddNotifications (new Contract ()
                .Requires ()
                .IsNullOrEmpty (CardHolderName, "CreatePayPalSubscriptionCommand.TransactionCode", "O código pagamento deve conter o código de transação.")
                .IsNullOrEmpty (CardNumber, "CreatePayPalSubscriptionCommand.CardNumber", "O código pagamento deve conter o código de transação.")
                .IsNullOrEmpty (LastTransactionNumber, "CreatePayPalSubscriptionCommand.LastTransactionNumber", "O código pagamento deve conter o código de transação."));
        }
    }
}