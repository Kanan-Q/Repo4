using System.ComponentModel.DataAnnotations;

namespace WebAppTest4.Models
{
    public class Employee : BaseEntity
    {
        [MaxLength(10)]
        [MinLength(4)]
        public string Name { get; set; }
        [MaxLength(15)]
        [MinLength(4)]
        public string Surname { get; set; }
        public string Photo {  get; set; }
        public int Age { get; set; }
        public int DepartmentId {  get; set; }
        public Department Department { get; set; }
    }
}
