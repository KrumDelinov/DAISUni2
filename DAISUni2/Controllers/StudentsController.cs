using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services.Contracts;
using BusinessLogic.DomainModels.StudentDomainModel;
using DataAccsess.Services.Contracts;
using EfRepsitory;

namespace DAISUni2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentBL studentBL;
        private readonly IEntityRepository repository;

        public StudentsController(IStudentBL studentBL, IEntityRepository repository)
        {
            this.studentBL = studentBL;
            this.repository = repository;
        }

        // GET: Students
        public IActionResult Index()
        {
            var viewModel = new AllSudentsViewModel();
            var students = studentBL.GetAllNoDeletedStudnts();
            viewModel.Students = students;
            return View(viewModel);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var student = await studentBL.DetailsStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,BirthDate")] CreateStudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                var newStudent = studentBL.CreateStudent(student);
                repository.Add(newStudent);
                await repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var student = await studentBL.EditdByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            //ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Email", student.UsersId);
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,BirthDate,CreatedOn")] EditStudentViewModel student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var updateStudent = studentBL.UpdateStudent(student);
                    repository.Update(updateStudent);
                    await repository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (student != null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
           
            return View(student);
        }

        //// GET: Students/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Students == null)
        //    {
        //        return NotFound();
        //    }

        //    var student = await _context.Students
        //        .Include(s => s.Users)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(student);
        //}

        //// POST: Students/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Students == null)
        //    {
        //        return Problem("Entity set 'DAISUni2Context.Students'  is null.");
        //    }
        //    var student = await _context.Students.FindAsync(id);
        //    if (student != null)
        //    {
        //        _context.Students.Remove(student);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

       
      
    }
}
