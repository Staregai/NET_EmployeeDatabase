using NET_EmployeeDatabase.Domain.Interfaces;
using NET_EmployeeDatabase.Domain.Models;

namespace NET_EmployeeDatabase.Domain.Services
{
    public class VacationService : IVacationService
    {
        private readonly DateTime _today = DateTime.Today;

        public int CountFreeDays(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
            if (employee.VacationPackage == null)
                throw new ArgumentNullException(nameof(employee.VacationPackage));
            if (employee.Vacations == null)
                throw new ArgumentNullException(nameof(employee.Vacations));

            int usedDays = employee.Vacations
                .Where(v => v.DateUntil < _today)
                .Sum(v => (v.DateUntil - v.DateSince).Days + 1);

            return Math.Max(0, employee.VacationPackage.GrantedDays - usedDays);
        }

        public bool CanRequestVacation(Employee employee)
        {
            try
            {
                return CountFreeDays(employee) > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}