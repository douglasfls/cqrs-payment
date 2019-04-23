using System;
using System.Linq;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Services;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers {
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>, IHandler<CreatePayPalSubscriptionCommand>, IHandler<CreateCreditCardSubscriptionCommand> {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;
        private Name name;
        private Document document;
        private Address address;
        private Email email;
        private Student student;
        private Subscription subscription;

        public SubscriptionHandler (IStudentRepository studentRepository, IEmailService emailService) {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle (CreateBoletoSubscriptionCommand command) {
            var payment = new BoletoPayment (command.PaiedDate, command.ExpireDate, command.Total, command.TotalPayed, address, command.Payer, document, email, command.BarCode, command.BoletoNumber);
            return CreateSubscription (command, payment);
        }
        public ICommandResult Handle (CreatePayPalSubscriptionCommand command) {
            var payment = new PayPalPayment (command.PaiedDate, command.ExpireDate, command.Total, command.TotalPayed, address, command.Payer, document, email, command.TransactionCode);
            return CreateSubscription (command, payment);
        }

        public ICommandResult Handle (CreateCreditCardSubscriptionCommand command) {
            var payment = new CreditCardPayment (command.PaiedDate, command.ExpireDate, command.Total, command.TotalPayed, address, command.Payer, document, email, command.CardHolderName, command.CardNumber, command.LastTransactionNumber);
            return CreateSubscription (command, payment);
        }

        private ICommandResult CreateSubscription (CreateSubscriptionCommand command, Payment payment) {
            command.Validate ();
            AddNotifications (command.Notifications);
            if (_studentRepository.EmailExists (command.Email)) AddNotification ("Email", "E-mail já cadastrado.");
            if (_studentRepository.DocumentExists (command.Document)) AddNotification ("Document", "Documento já cadastrado.");
            GenerateGeneralValueObjects (command);
            GenerateSubscription (command);
            subscription.AddPayment (payment);
            student.AddSubscription (subscription);
            AddNotifications (student.Notifications);
            if (Invalid) return new CommandResult (false, string.Join (",", Notifications.Select (p => p.Message)));
            _studentRepository.CreateSubscription (student);
            SendWelcomeEmail ();
            return new CommandResult (true, "Assinatura cadastrada com sucesso!");
        }

        private void GenerateGeneralValueObjects (CreateSubscriptionCommand command) {
            name = new Name (command.FirstName, command.LastName);
            document = new Document (command.Document, command.DocumentType);
            address = new Address (command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            email = new Email (command.Email);
            student = new Student (name, document, email, address);
        }
        private void GenerateSubscription (CreateSubscriptionCommand command) {
            subscription = new Subscription (DateTime.Now.AddMonths (1));
        }
        private void SendWelcomeEmail () {
            _emailService.SendEmail (email.Address, "Bem Vindo", "Aqui vai algum texto");
        }
    }
}