using System;
using PaymentContext.Services;

namespace PaymentContext.Tests.Mock {
    public class FakeEmailService : IEmailService {
        public void SendEmail (string to, string subject, string body) { }
    }
}