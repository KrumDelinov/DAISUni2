using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccsess.DbContexts;
using DataAccsess.Entities;
using BusinessLogic.Services.Contracts;
using BusinessLogic.DomainModels.ModuleDomainModels;
using EfRepsitory;

namespace DAISUni2.Controllers
{
    public class ModulesController : Controller
    {
        private readonly IModuleBL moduleBL;
        private readonly IEntityRepository repository;

        public ModulesController(IModuleBL moduleBL, IEntityRepository repository)
        {
            this.moduleBL = moduleBL;
            this.repository = repository;
        }

        // GET: Modules
        public IActionResult Index()
        {
            var modiles = moduleBL.AllModules();
            var viewModel = new AllModuleViewModel
            {
                Modules = modiles,
            };
            
            
            return View( viewModel);
        }

        // GET: Modules/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = moduleBL.Details(id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Modules/Create
        public IActionResult Create()
        {
            var viewModel = moduleBL.CreateModuleViewModel();
            ViewData["CoursesId"] = new SelectList(viewModel.Courses, "Id","Name","Description");
            return View();
        }

        // POST: Modules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,CoursesId")] CreateModuleViewModel @module)
        {
            if (ModelState.IsValid)
            {
                var newModule = moduleBL.CreateModule(@module);
                repository.Add(newModule);
                await repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoursesId"] = new SelectList(@module.Courses, "Id", "Description", @module.CoursesId);
            return View(@module);
        }

        // GET: Modules/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var @module = moduleBL.EditModuleViewModel(id);
            if (@module == null)
            {
                return NotFound();
            }
            ViewData["CoursesId"] = new SelectList(@module.Courses, "Id", "Name", @module.CoursesId);
            return View(@module);
        }

        // POST: Modules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id","Name", "Description", "CoursesId", "Courses")] ModuleEditViewModel @module)
        {
            if (id != @module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var editedModule = moduleBL.Edit(@module);
                    repository.Update(editedModule);
                    await repository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (@module.Id != null)
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
            ViewData["CoursesId"] = new SelectList(@module.Courses, "Id", "Name", @module.CoursesId);
            return View(@module);
        }

        //// GET: Modules/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Modules == null)
        //    {
        //        return NotFound();
        //    }

        //    var @module = await _context.Modules
        //        .Include(@ => @.Courses)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (@module == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(@module);
        //}

        //// POST: Modules/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Modules == null)
        //    {
        //        return Problem("Entity set 'DAISUni2Context.Modules'  is null.");
        //    }
        //    var @module = await _context.Modules.FindAsync(id);
        //    if (@module != null)
        //    {
        //        _context.Modules.Remove(@module);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ModuleExists(int id)
        //{
        //  return (_context.Modules?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
