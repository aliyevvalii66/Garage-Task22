using BigonWebShoppingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Diagnostics.CodeAnalysis;

namespace BigonWebShoppingApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            var parent = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.ParentCategoryId);
            category.ParentCategory = parent;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category is null)
                return NotFound();
            ViewBag.Categories = _context.Categories.ToList();
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            var dbCate = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (dbCate == null)
                return NotFound();

            dbCate.Name = category.Name;
            dbCate.ParentCategory = category.ParentCategory;
            dbCate.ParentCategoryId = category.ParentCategoryId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _context.Categories.FindAsync(id);
            if(data == null)
                return NotFound();
            _context.Categories.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
