using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppTest4.DAL;

namespace WebAppTest4.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {


        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            return View(await _context.Employees.ToListAsync());
        }
        public async Task<IActionResult> Contact()
        {
            return View();
        }
        public async Task<IActionResult> Testimonital()
        {
            return View();
        }


    }
}
