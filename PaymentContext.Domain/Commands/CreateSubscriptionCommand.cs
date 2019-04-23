using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands {
    public abstract class CreateSubscriptionCommand : Notifiable, ICommand {

        public CreateSubscriptionCommand () { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public EDocumentType DocumentType { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public DateTime PaiedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPayed { get; set; }
        public string Payer { get; set; }

        public void Validate () {
            AddNotifications (new Contract ()
                .Requires ()
                .HasMinLen (FirstName, 3, "Name.FirstName", "Nome deve conter ao menos 3 caracteres")
                .HasMinLen (LastName, 3, "Name.LastName", "O ultimo nome deve conter ao menos 3 caracteres")
                .HasMaxLen (FirstName, 50, "Name.FirstName", "O nome deve conter no máximo 50 caracters")
                .IsEmail (Email, "Email.Address", "E-mail inválido."));
        }
    }
}