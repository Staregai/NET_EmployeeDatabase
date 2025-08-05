public interface IVacationService
{
    int CountFreeDays(Employee employee);
    bool CanRequestVacation(Employee employee);
}