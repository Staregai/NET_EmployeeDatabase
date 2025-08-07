using NET_EmployeeDatabase.Domain.Interfaces;
using NET_EmployeeDatabase.Domain.Models;

namespace NET_EmployeeDatabase.Domain.Services
{
    public class VacationService : IVacationService
    {
        private readonly DateTime _today = DateTime.Today;

        public int CountFreeDays(Employee employee)
        {
            if (employee == null || employee.VacationPackage == null)
                throw new ArgumentNullException(nameof(employee));

            int usedDays = employee.Vacations
                .Where(v => v.DateUntil < _today)
                .Sum(v => (v.DateUntil - v.DateSince).Days + 1);

            return Math.Max(0, employee.VacationPackage.GrantedDays - usedDays);
        }

        public bool CanRequestVacation(Employee employee)
        {
            return CountFreeDays(employee) > 0;
        }
    }
}