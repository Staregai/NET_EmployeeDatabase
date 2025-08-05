public interface IEmployeeStructureService
{
    List<EmployeeStructure> BuildStructure(List<Employee> employees);
    int? GetSuperiorRank(int employeeId, int superiorId);
}