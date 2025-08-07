using System;

namespace NET_EmployeeDatabase.Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SuperiorId { get; set; }
        public int TeamId { get; set; }
        public int VacationPackageId { get; set; }

        public virtual Team Team { get; set; }
        public virtual VacationPackage VacationPackage { get; set; }
        public virtual List<Vacation> Vacations { get; set; } = new();

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Employee name cannot be empty.", nameof(Name));
            if (Team == null)
                throw new ArgumentNullException(nameof(Team), "Employee must have a team assigned.");
            if (VacationPackage == null)
                throw new ArgumentNullException(nameof(VacationPackage), "Employee must have a vacation package assigned.");
            if (Vacations == null)
                throw new ArgumentNullException(nameof(Vacations), "Employee vacations list cannot be null.");
        }
    }
}