using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects {
    public class Name : ValueObject {
        public Name (string firstName, string lastName) {
            FirstName = firstName;
            LastName = lastName;

            if (string.IsNullOrEmpty (FirstName))
                AddNotification ("Name.FirstName", "Nome inválido");

            AddNotifications (new Contract ()
                .Requires ()
                .HasMinLen (FirstName, 3, "Name.FirstName", "Nome deve conter ao menos 3 caracteres")
                .HasMinLen(LastName, 3, "Name.LastName", "O ultimo nome deve conter ao menos 3 caracteres")
                .HasMaxLen(FirstName, 50, "Name.FirstName", "O nome deve conter no máximo 50 caracters"));
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get => $"{LastName}, {FirstName}"; }
    }
}