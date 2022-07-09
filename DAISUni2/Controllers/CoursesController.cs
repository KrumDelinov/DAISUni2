using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.DomainModels.CourseDomainMoel;
using BusinessLogic.Services.Contracts;
using BusinessLogic.Services;
using EfRepsitory;

namespace DAISUni2.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseBL courseBL;
        private readonly IEntityRepository repository;

        public CoursesController(ICourseBL courseBL, IEntityRepository repository)
        {
            this.courseBL = courseBL;
            this.repository = repository;
        }

        // GET: Courses
        public IActionResult Index()
        {
            var errMsg = TempData["ErrorMessage"] as string;
            var viewModel = new AllCoursesViewModel();
            var courses = courseBL.AllCourses();
            viewModel.Courses = courses;
            return View(viewModel);
        }
        // GET: Courses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = courseBL.CourseEditViewMdel(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            if (course.IsRunning)
            {
                this.TempData["InfoMessage"] = "Course is running";
                return RedirectToAction("Index");
            }
            ViewData["TeachersId"] = new SelectList(course.Teachers, "Id", "FullName", course.TeachersId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Difficulty,TeachersId,CreatedOn,StartDate,EndDate")] CourseEditViewModel course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await courseBL.EditAsync(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (course.Id != null)
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
            ViewData["TeachersId"] = new SelectList(course.Teachers, "Id", "FullName", course.TeachersId);
            return View(course);
        }

        public IActionResult AddStudents(int id)
        {
            var view = courseBL.ViewModel(id);
            ViewData["StudentsId"] = new SelectList(view.FreeStudents, "Id", "FullName", view.StudentId);

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudents(int id, [Bind("Id,Name,StudentId")] AddStudentsToCourseViewModel student)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    courseBL.AddStudentToCourse(student.Id, student.StudentId);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (student.StudentId != null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }
            ViewData["StudentsId"] = new SelectList(student.FreeStudents, "Id", "FullName", student.StudentId);

            return View(student);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = courseBL.Course(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            var courseView = courseBL.CreateCourseViewModel();
            ViewData["TeachersId"] = new SelectList(courseView.Teachers, "Id", "FullName");
            return View();
        }

        public IActionResult CourseStudents(int id)
        {
            var students = courseBL.StudentViewModels(id);
            var viewModel = new CourseStudentsViewModel { Students = students };
            return View(viewModel);
        }

        public IActionResult CourseModules(int id)
        {
            var modules = courseBL.ModuleViewModels(id);
            var viewModel = new CourseModulesViewModel { Modules = modules };
            return View(viewModel);
        }

        public IActionResult CourseMaterials(int id)
        {
            var view = courseBL.CourseMaterials(id);
            return View(view);
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Difficulty,TeachersId,StartDate,EndDate")] CreateCourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                var newCourse = courseBL.CreateCourse(course);
                repository.Add(newCourse);
                await repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeachersId"] = new SelectList(course.Teachers, "Id", "FullName", course.TeachersId);
            return View(course);
        }

     

        //// GET: Courses/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Courses == null)
        //    {
        //        return NotFound();
        //    }

        //    var course = await _context.Courses
        //        .Include(c => c.Teachers)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(course);
        //}

        //// POST: Courses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Courses == null)
        //    {
        //        return Problem("Entity set 'DAISUni2Context.Courses'  is null.");
        //    }
        //    var course = await _context.Courses.FindAsync(id);
        //    if (course != null)
        //    {
        //        _context.Courses.Remove(course);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CourseExists(int id)
        //{
        //  return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
