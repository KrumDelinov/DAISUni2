using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccsess.Entities;
using BusinessLogic.DomainModels.TeacherDomainModels;
using BusinessLogic.Services.Contracts;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using EfRepsitory;

namespace DAISUni2.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherBL teacherBL;
        private readonly IEntityRepository repository;

        public TeachersController(ITeacherBL teacherBL, IEntityRepository repository)
        {
            this.teacherBL = teacherBL;
            this.repository = repository;
        }

        // GET: Teachers
        public IActionResult Index()
        {
            var teachers = new AllTeachersViewModel();
            teachers.Teachers = teacherBL.GetTeachers();
            return View(teachers);
        }

        [HttpPost]
        public IActionResult Index(IFormCollection obj)
        {
            string reportname = $"Teachers_{Guid.NewGuid():N}.xlsx";
            var list = teacherBL.GetTeachers().ToList();

            if (list.Count > 0)
            {
                var exportbytes = ExporttoExcel(list, reportname);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
            }
            else
            {
                TempData["Message"] = "No Data to Export";
                return View();
            }

        }

        // GET: Teachers/Details/5
        public IActionResult Details(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var teacher = teacherBL.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            var view = new CreateTeacherViewModel();
            return View(view);
        }

        // POST: Teachers/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,HiredOn")] CreateTeacherViewModel teacher)
        {
            if (ModelState.IsValid)
            {
                var newTeacher = teacherBL.CreateTeacher(teacher);
                repository.Add(newTeacher);
                await repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task< IActionResult> Edit(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var teacher = await teacherBL.FindTeacherByIdAsynk(id);
            if (teacher == null)
            {
                return NotFound();
            }
            //ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Email", teacher.UsersId);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,HiredOn")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repository.Update(teacher);
                    await repository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!teacherBL.TeacherExists(teacher.Id))
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
            return View(teacher);
        }

        //// GET: Teachers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Teachers == null)
        //    {
        //        return NotFound();
        //    }

        //    var teacher = await _context.Teachers
        //        .Include(t => t.Users)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (teacher == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(teacher);
        //}

        //// POST: Teachers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Teachers == null)
        //    {
        //        return Problem("Entity set 'DAISUni2Context.Teachers'  is null.");
        //    }
        //    var teacher = await _context.Teachers.FindAsync(id);
        //    if (teacher != null)
        //    {
        //        _context.Teachers.Remove(teacher);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


        private byte[] ExporttoExcel<T>(List<T> table, string filename)
        {
            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromCollection(table, true, TableStyles.Light1);
            return pack.GetAsByteArray();
        }
    }
}
