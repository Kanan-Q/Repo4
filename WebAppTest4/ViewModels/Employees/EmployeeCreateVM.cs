using System.ComponentModel.DataAnnotations;
using WebAppTest4.Models;

namespace WebAppTest4.ViewModels.Employees
{
    public class EmployeeCreateVM
    {
        [MaxLength(10)]
        [MinLength(4)]
        public string Name { get; set; }
        [MaxLength(15)]
        [MinLength(4)]
        public string Surname { get; set; }
        public string CoverPhoto { get; set; }

        public IFormFile Photo { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }
    }
}
