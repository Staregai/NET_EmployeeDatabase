
using NET_EmployeeDatabase.Domain.Models;
using NET_EmployeeDatabase.Domain.Services;
using NUnit.Framework;
using System;

namespace NET_EmployeeDatabase.Tests
{
    [TestFixture]
    public class VacationServiceTests
    {
        private VacationService _service;

        [SetUp]
        public void Setup()
        {
            _service = new VacationService();
        }

        [Test]
        public void employee_can_request_vacation()
        {
            var employee = new Employee
            {
                VacationPackage = new VacationPackage { GrantedDays = 10 },
                Vacations = new List<Vacation>
            {
                new Vacation { DateSince = DateTime.Today.AddDays(-10), DateUntil = DateTime.Today.AddDays(-5) }
            }
            };

            Assert.IsTrue(_service.CanRequestVacation(employee));

        }

        [Test]
        public void employee_cant_request_vacation()
        {
            var employee = new Employee
            {
                VacationPackage = new VacationPackage { GrantedDays = 5 },
                Vacations = new List<Vacation>
            {
                new Vacation { DateSince = DateTime.Today.AddDays(-10), DateUntil = DateTime.Today.AddDays(-5) },
                new Vacation { DateSince = DateTime.Today.AddDays(-4), DateUntil = DateTime.Today.AddDays(-2) }
            }
            };

            Assert.IsFalse(_service.CanRequestVacation(employee));

        }
    }
}