﻿using CmsShoppingCart.InfraStructure;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly CmsShoppingCartContext context;
        public PagesController(CmsShoppingCartContext context)
        {
            this.context = context;
        }
        // GET /admin/Pages
        public async Task<IActionResult> Index()
        {
           IQueryable<Page> pages = from p in context.Pages orderby p.Sorting select p;

            List<Page> pagesList = await pages.ToListAsync();

            return View(pagesList);
        }

        // GET /admin/Pages/details/id
        public async Task<IActionResult> Details(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if(page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET /admin/Pages/create/id
        public IActionResult Create() => View();

        // POST /admin/Pages/create
        [HttpPost]
        public async Task<IActionResult> Create(Page page)
        {
           if (ModelState.IsValid)
           {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;

                var slug = await context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The title already exists.");
                    return View(page);
                }

                context.Add(page);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
           }
            return View(page);
        }
    }
}
