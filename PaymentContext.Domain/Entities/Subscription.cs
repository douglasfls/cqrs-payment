using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public sealed class Subscription : Entity {
        IList<Payment> _payments;
        public Subscription (DateTime? expireDate) {
            Active = true;
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            _payments = new List<Payment> ();
        }

        public bool Active { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get => _payments.ToArray (); }

        public void AddPayment (Payment payment) {
            AddNotifications (new Contract ()
                .Requires ()
                .IsGreaterThan (DateTime.Now, payment.PaiedDate, "Subscription.Payments", "A data do pagamento é inválida"));

            if (Valid)
                _payments.Add (payment);
        }

        public void Activate () {
            Active = true;
            LastUpdateDate = DateTime.Now;
        }

        public void Inactivate () {
            Active = false;
            LastUpdateDate = DateTime.Now;
        }
    }
}