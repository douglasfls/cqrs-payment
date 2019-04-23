using Flunt.Validations;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands {
    public class CreateBoletoSubscriptionCommand : CreateSubscriptionCommand, ICommand {

        public CreateBoletoSubscriptionCommand () {

        }
        public string BarCode { get; set; }
        public string BoletoNumber { get; set; }

        public new void Validate () {
            base.Validate ();
            AddNotifications (new Contract ()
                .Requires ()
                .IsNullOrEmpty (BarCode, "CreateBoletoSubscriptionCommand.BarCode", "O código de barras é necessário.")
                .IsNullOrEmpty (BoletoNumber, "CreateBoletoSubscriptionCommand.BoletoNumber", "O numero do boleto é necessário."));
        }
    }
}