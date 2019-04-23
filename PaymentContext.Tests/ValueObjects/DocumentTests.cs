using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects {
    [TestClass]
    public class DocumentTests {
        [TestMethod]
        public void When_I_Enter_An_Invalid_Cpf_And_It_Is_Valid () {
            var document = new Document ("34843436801", EDocumentType.Cpf);
            var expected = 0;
            var actual = document.Notifications.Count;
            Assert.AreEqual (expected, actual);
        }

        [TestMethod]
        public void When_I_Enter_An_Invalid_Cpf_And_It_Is_Invalid () {
            var document = new Document ("3484343680", EDocumentType.Cpf);
            var expected = 1;
            var actual = document.Notifications.Count;
            Assert.AreEqual (expected, actual);
        }
    }
}