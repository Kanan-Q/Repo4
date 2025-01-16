using System.ComponentModel.DataAnnotations;

namespace WebAppTest4.Models
{
    public class Department:BaseEntity
    {
        [MaxLength(10)]
        public string? DepartmentName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
