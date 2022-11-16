using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Queries
{
    public static class StudentQueries {
        public static Func<Student, bool> GetStudent (string document) {
            Func<Student, bool> result = x => x.Document.Number == document;
            return result;
        }
    }
}