using Microsoft.EntityFrameworkCore;
using WebAppTest4.Models;

namespace WebAppTest4.DAL
{
    public class AppDbContext:DbContext
    {
        public DbSet<Employee> Employees {  get; set; }
        public DbSet<Department> Departments {  get; set; }

        public AppDbContext(DbContextOptions opt):base(opt){}

    }
}
