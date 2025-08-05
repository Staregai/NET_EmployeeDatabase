using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NET_EmployeeDatabase.Domain.Queries
{
    //Poniższe kwerendy zakładją istnienie klasy AppDbContext, będącej kontekstem bazy danych
    public class VacationQueries
    {
        private readonly AppDbContext context;

        public VacationQueries(AppDbContext dbContext)
        {
            context = dbContext;
        }

        // 2a) Lista pracowników z zespołu ".NET" z co najmniej jednym urlopem w 2019 roku
        public List<Employee> GetDotNetEmployeesWithVacationIn2019()
        {
            return context.Employees
                .Include(e => e.Team)
                .Include(e => e.Vacations)
                .Where(e => e.Team.Name == ".NET"
                    && e.Vacations.Any(v =>
                        v.DateSince.Year == 2019 || v.DateUntil.Year == 2019))
                .ToList();
        }

        // 2b) Lista pracowników + liczba zużytych dni urlopowych w bieżącym roku
        public List<(Employee Employee, int UsedDays)> GetUsedVacationDaysForCurrentYear()
        {
            var today = DateTime.Today;
            var currentYear = today.Year;

            return context.Employees
                .Select(e => new
                {
                    Employee = e,
                    UsedDays = e.Vacations
                        .Where(v => v.DateUntil < today && v.DateSince.Year == currentYear)
                        .Sum(v => EF.Functions.DateDiffDay(v.DateSince, v.DateUntil) + 1)
                })
                .AsEnumerable()
                .Select(x => (x.Employee, x.UsedDays))
                .ToList();
        }

        // 2c) Lista zespołów, których pracownicy nie złożyli żadnego urlopu w 2019 roku
        public List<Team> GetTeamsWithNoVacationsIn2019()
        {
            return context.Teams
                .Where(t => !context.Employees
                    .Where(e => e.TeamId == t.Id)
                    .SelectMany(e => e.Vacations)
                    .Any(v => v.DateSince.Year == 2019 || v.DateUntil.Year == 2019))
                .ToList();
        }
    }

}
