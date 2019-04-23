using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Queries;
using PaymentContext.Tests.Mock;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests {

        [TestMethod]
        public void TestQueryStudent () {
            var studentRepository = new FakeStudentRepository ();
            var expr = StudentQueries.GetStudent ("34843436801");
            var result = studentRepository.GetStudents (expr);
            var expected = 1;
            var actual = result.Count ();
            Assert.AreEqual (expected, actual);
        }
    }
}