using System;
using System.Collections.Generic;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories {
    public interface IStudentRepository {
        bool DocumentExists (string document);
        bool EmailExists (string email);
        void CreateSubscription (Student student);
        IEnumerable<Student> GetStudents (Func<Student, bool> expr);
    }
}