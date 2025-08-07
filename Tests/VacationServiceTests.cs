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

        [Test]
        public void count_free_days_throws_on_null_employee()
        {
            Assert.Throws<ArgumentNullException>(() => _service.CountFreeDays(null));
        }

        [Test]
        public void count_free_days_throws_on_null_vacation_package()
        {
            var employee = new Employee
            {
                VacationPackage = null,
                Vacations = new List<Vacation>()
            };
            Assert.Throws<ArgumentNullException>(() => _service.CountFreeDays(employee));
        }

        [Test]
        public void count_free_days_throws_on_null_vacations()
        {
            var employee = new Employee
            {
                VacationPackage = new VacationPackage { GrantedDays = 10 },
                Vacations = null
            };
            Assert.Throws<ArgumentNullException>(() => _service.CountFreeDays(employee));
        }
    }
}