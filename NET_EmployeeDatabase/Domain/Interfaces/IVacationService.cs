using NET_EmployeeDatabase.Domain.Models;
namespace NET_EmployeeDatabase.Domain.Interfaces
{
    public interface IVacationService
    {
        int CountFreeDays(Employee employee);
        bool CanRequestVacation(Employee employee);
    }
}