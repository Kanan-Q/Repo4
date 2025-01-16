using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppTest4.DAL;
using WebAppTest4.Models;
using WebAppTest4.ViewModels.Departments;
using WebAppTest4.ViewModels.Employees;

namespace WebAppTest4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.Include(x => x.Department).ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVM vm)
        {

            ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
           
            //if (!ModelState.IsValid) return BadRequest();

            if (!vm.Photo.ContentType.StartsWith("image"))
            {
                ModelState.AddModelError("Photo", "Image deyil");
            }

            string filename = Path.GetRandomFileName() + Path.GetExtension(vm.Photo.FileName);
            using (Stream s = System.IO.File.Create(Path.Combine(_env.WebRootPath, "images", "Employees", filename)))
            {
                await vm.Photo.CopyToAsync(s);
            }
            Employee d = new Employee()
            {
                DepartmentId = vm.DepartmentId,
                Age = vm.Age,
                Name = vm.Name,
                Surname = vm.Surname,
                Photo = filename
            };
            await _context.Employees.AddAsync(d);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
            if (!id.HasValue) return BadRequest();
            var data = await _context.Employees.Where(x => x.Id == id).Select(x => new EmployeeUpdateVM
            {
                DepartmentId = x.DepartmentId,
                Age = x.Age,
                Name = x.Name,
                Surname = x.Surname,
                CoverPhoto = x.Photo,
            }).FirstOrDefaultAsync();
            if (data is null) return NotFound();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, EmployeeUpdateVM vm)
        {


            if (!id.HasValue) return NotFound();

            if (!vm.Photo.ContentType.StartsWith("image"))
            {
                ModelState.AddModelError("Photo", "Image deyil");
            }
            ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
            var data = await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();

            string oldname = Path.Combine(_env.WebRootPath, "images", "Employees", data.Photo);
            using (Stream s = System.IO.File.Create(oldname))
            {

                await vm.Photo!.CopyToAsync(s);
            }
            data.Surname = vm.Surname;
            data.Name = vm.Name;
            data.Age = vm.Age;
            data.DepartmentId = vm.DepartmentId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Employees.FindAsync(id);
            if (data is null) return NotFound();
            _context.Employees.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Employees.FindAsync(id);
            if (data is null) return NotFound();
            data.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Employees.FindAsync(id);
            if (data is null) return NotFound();
            data.IsDeleted = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
