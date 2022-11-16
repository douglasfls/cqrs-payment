using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests {
        [TestMethod]
        public void When_I_Enter_A_Name_And_It_Is_Valid () {
            var name = new Name ("Douglas", "Lopes");
            var document = new Document ("34843436801", EDocumentType.Cpf);
            var email = new Email ("abc@cde.com");
            var address = new Address ("", "", "", "", "", "", "");
            var student = new Student (name, document, email, address);

            var expected = 0;
            var actual = student.Notifications.Count;

            Assert.AreEqual (expected, actual);
        }

        [TestMethod]
        public void TestaSubscription () {
            var name = new Name ("Douglas", "Lopes");
            var document = new Document ("34843436801", EDocumentType.Cpf);
            var email = new Email ("abc@cde.com");
            var address = new Address ("", "", "", "", "", "", "");
            var student = new Student (name, document, email, address);

            var expected = 0;
            var paypal = new PayPalPayment (DateTime.Now, DateTime.Now, 100, 200, address, "1234", document, email, "123");
            var sub = new Subscription (DateTime.Now);
            sub.AddPayment (paypal);
            sub.Activate ();

            student.AddSubscription (sub);

            student.Notifications.AsParallel ().ForAll (p => Console.WriteLine (p.Message));

            Assert.AreEqual (expected, student.Notifications.Count);
        }

        [TestMethod]
        public void TestaSubscription2 () {
            var name = new Name ("Douglas", "Lopes");
            var document = new Document ("34843436801", EDocumentType.Cpf);
            var email = new Email ("abc@cde.com");
            var address = new Address ("", "", "", "", "", "", "");
            var student = new Student (name, document, email, address);

            var expected = 1;
            var sub = new Subscription (DateTime.Now);
            student.AddSubscription (sub);

            student.Notifications.ToList ().ForEach (p => Console.WriteLine (p.Message));

            Assert.AreEqual (expected, student.Notifications.Count);
        }
    }
}