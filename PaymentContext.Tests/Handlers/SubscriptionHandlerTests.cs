using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mock;

namespace PaymentContext.Tests.Handlers {
    [TestClass]
    public class HandlerTests {
        [TestMethod]
        public void TestHandlePaypal () {
            var handler = new SubscriptionHandler (new FakeStudentRepository (), new FakeEmailService ());
            var command = new CreateBoletoSubscriptionCommand {
                BarCode = "123456",
                BoletoNumber = "",
                City = "",
                Country = "",
                Document = "89503035074",
                DocumentType = EDocumentType.Cpf,
                Email = "Abc@cde.com",
                ExpireDate = DateTime.Now,
                FirstName = "Douglas",
                LastName = "Lopes",
                Neighborhood = "",
                Number = "",
                PaiedDate = DateTime.Now,
                Payer = "",
                State = "",
                Street = "",
                Total = 500,
                TotalPayed = 500,
                ZipCode = ""
            };
            var result = handler.Handle (command);
            var expected = true;
            var actual = result.Success;
            Assert.AreEqual (expected, actual);
        }
    }
}