using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities {
    public class Student : Entity {
        IList<Subscription> _subscriptions;
        public Student (Name name, Document document, Email email, Address address) {
            Name = name;
            Document = document;
            Email = email;
            Address = address;
            _subscriptions = new List<Subscription> ();

            AddNotifications (name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get => _subscriptions.ToArray (); }

        public void AddSubscription (Subscription subscription) {
            var hasSubscriptionActive = false;
            hasSubscriptionActive = _subscriptions.Any (p => p.Active);

            AddNotifications (new Contract ()
                .IsFalse (hasSubscriptionActive, "Student.Subscrption", "Você jé possui uma assinatura ativa")
                .AreNotEquals(subscription.Payments.Count, 0, "Student.Subscriptions", "A assinatura deve conter pagamento"));

            if (Valid)
                _subscriptions.Add (subscription);
        }
    }
}