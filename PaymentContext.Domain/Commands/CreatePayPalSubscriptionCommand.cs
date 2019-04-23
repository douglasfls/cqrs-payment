using Flunt.Validations;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands {
    public class CreatePayPalSubscriptionCommand : CreateSubscriptionCommand, ICommand {

        public CreatePayPalSubscriptionCommand () {

        }
        public string TransactionCode { get; set; }
        public new void Validate () {
            base.Validate ();
            AddNotifications (new Contract ()
                .Requires ()
                .IsNotNullOrEmpty (TransactionCode, "CreatePayPalSubscriptionCommand.TransactionCode", "O código pagamento deve conter o código de transação."));
        }
    }
}