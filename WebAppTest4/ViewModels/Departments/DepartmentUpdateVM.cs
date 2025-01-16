using System.ComponentModel.DataAnnotations;

namespace WebAppTest4.ViewModels.Departments
{
    public class DepartmentUpdateVM
    {
        [MaxLength(10)]
        public string? DepartmentName { get; set; }
    }
}
