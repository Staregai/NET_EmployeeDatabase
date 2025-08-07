using NET_EmployeeDatabase.Domain.Interfaces;
using NET_EmployeeDatabase.Domain.Models;

namespace NET_EmployeeDatabase.Domain.Services
{
    public class EmployeeStructureService : IEmployeeStructureService
    {
        private List<EmployeeStructure> _structure;

        public List<EmployeeStructure> BuildStructure(List<Employee> employees)
        {
            if (employees == null)
                throw new ArgumentNullException(nameof(employees));

            var structure = new List<EmployeeStructure>();
            var lookup = employees.ToDictionary(e => e.Id);

            foreach (var emp in employees)
            {
                if (emp == null)
                    throw new ArgumentException("Employee list contains null element.", nameof(employees));

                int rank = 1;
                var current = emp;
                while (current.SuperiorId.HasValue && lookup.ContainsKey(current.SuperiorId.Value))
                {
                    structure.Add(new EmployeeStructure
                    {
                        EmployeeId = emp.Id,
                        SuperiorId = current.SuperiorId.Value,
                        Rank = rank++
                    });

                    current = lookup[current.SuperiorId.Value];
                }
            }

            _structure = structure;
            return structure;
        }

        public int? GetSuperiorRank(int employeeId, int superiorId)
        {
            if (_structure == null)
                throw new InvalidOperationException("Structure has not been built yet.");

            return _structure
                .FirstOrDefault(s => s.EmployeeId == employeeId && s.SuperiorId == superiorId)
                ?.Rank;
        }
    }
}