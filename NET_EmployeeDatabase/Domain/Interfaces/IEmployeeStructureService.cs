using NET_EmployeeDatabase.Domain.Models;

namespace NET_EmployeeDatabase.Domain.Interfaces
{
    public interface IEmployeeStructureService
    {
        List<EmployeeStructure> BuildStructure(List<Employee> employees);
        int? GetSuperiorRank(int employeeId, int superiorId);
    }
}