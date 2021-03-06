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
using BusinessLogic.DomainModels.MaterialDomainModels;
using EfRepsitory;

namespace DAISUni2.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly IMaterialBL materialBL;
        private readonly IEntityRepository repository;

        public MaterialsController(IMaterialBL materialBL, IEntityRepository repository)
        {
            this.materialBL = materialBL;
            this.repository = repository;
        }

        // GET: Materials
        public IActionResult Index()
        {
            var view = materialBL.AllMaterials();
            return View(view);
        }

        // GET: Materials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var material = materialBL.MaterialViewModel(id.Value);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // GET: Materials/Create
        public IActionResult Create()
        {
            var courses = materialBL.Courses();
            ViewData["CoursesId"] = new SelectList(courses, "Id", "Name");
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialType,MaterialUrl,Content,CoursesId")] CreateMaterialViewModel material)
        {
            if (ModelState.IsValid)
            {
                var newMaterial = materialBL.Crate(material);
                repository.Add(newMaterial);
                await repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var courses = materialBL.Courses();
            ViewData["CoursesId"] = new SelectList(courses, "Id", "Name", material.CoursesId);
            return View(material);
        }

        //// GET: Materials/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Materials == null)
        //    {
        //        return NotFound();
        //    }

        //    var material = await _context.Materials.FindAsync(id);
        //    if (material == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CoursesId"] = new SelectList(_context.Courses, "Id", "Description", material.CoursesId);
        //    return View(material);
        //}

        //// POST: Materials/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,MaterialType,MaterialUrl,CoursesId")] Material material)
        //{
        //    if (id != material.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(material);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MaterialExists(material.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CoursesId"] = new SelectList(_context.Courses, "Id", "Description", material.CoursesId);
        //    return View(material);
        //}

        //// GET: Materials/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Materials == null)
        //    {
        //        return NotFound();
        //    }

        //    var material = await _context.Materials
        //        .Include(m => m.Courses)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (material == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(material);
        //}

        //// POST: Materials/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Materials == null)
        //    {
        //        return Problem("Entity set 'DAISUni2Context.Materials'  is null.");
        //    }
        //    var material = await _context.Materials.FindAsync(id);
        //    if (material != null)
        //    {
        //        _context.Materials.Remove(material);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MaterialExists(int id)
        //{
        //  return (_context.Materials?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
