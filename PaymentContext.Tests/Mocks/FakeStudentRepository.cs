using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Mock {
    public class FakeStudentRepository : IStudentRepository {
        public FakeStudentRepository () {
            _students = new List<Student> ();
            Feed ();
        }
        private List<Student> _students;
        public void CreateSubscription (Student student) {
            _students.Add (student);
        }

        public bool DocumentExists (string document) {
            return _students.Any (p => p.Document.Number == document);
        }

        public bool EmailExists (string email) {
            return _students.Any (p => p.Email.Address == email);
        }

        public IEnumerable<Student> GetStudents (Func<Student, bool> expr) {
            return _students.Where (expr);
        }

        private void Feed () {
            var name = new Name ("Fake", "Test");
            var document = new Document ("34843436801", EDocumentType.Cpf);
            var email = new Email ("fake@test.com");
            var address = new Address ("St1", "123", "neighborhood", "Sorocaba", "SP", "Brazil", "18040000");
            _students.Add (new Student (name, document, email, address));
        }
    }
}