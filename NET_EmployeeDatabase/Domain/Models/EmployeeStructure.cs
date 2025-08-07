namespace NET_EmployeeDatabase.Domain.Models
{
    public class EmployeeStructure
    {
        public int EmployeeId { get; set; }
        public int SuperiorId { get; set; }
        public int Rank { get; set; }
    }
}