using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using StudentPortal.web.Data;
using StudentPortal.web.Models;
using StudentPortal.web.Models.Entities;

namespace StudentPortal.web.Controllers
{
    public class StudentsController1 : Controller
    {
        private readonly Applicationdbcontex dbContex;

        public StudentsController1(Applicationdbcontex dbContex)
        {
            this.dbContex = dbContex;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewmodel)
        {
            var student = new Student
            {
                Name = viewmodel.Name,
                Email = viewmodel.Email,
                Phone = viewmodel.Phone,
                Subscribed = viewmodel.Subscribed,
            };
               await dbContex.students.AddAsync(student);
            await dbContex.SaveChangesAsync();

            return View();
           

        }
        [HttpGet]
        public async Task<IActionResult>List()
        {
            var student = await dbContex.students.ToListAsync();
            return View(student);
        }
        [HttpGet]
        public async Task<IActionResult>Edit(Guid id)
        {
            var student= await dbContex.students.FindAsync(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(Student viewmodel)
        {
            var student= await dbContex.students.FindAsync(viewmodel.Id);
            if (student is not null)
            {
                student.Name = viewmodel.Name;
                student.Email = viewmodel.Email;
                student.Phone = viewmodel.Phone;
                student.Subscribed = viewmodel.Subscribed;
                await dbContex.SaveChangesAsync();
            }
            return RedirectToAction("List" , "StudentsController1");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewmodel)
        {
            var student = await dbContex.students.AsNoTracking().FirstOrDefaultAsync(x=>x.Id== viewmodel.Id);
            
            if (student is not null)
            {
                 dbContex.students.Remove(viewmodel);
                await dbContex.SaveChangesAsync();
            }
            return RedirectToAction("List","StudentsController1");
        }
    }
}
