using Microsoft.AspNetCore.Mvc;
using Midterm.Data;
using Midterm.Models;

namespace Midterm.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;
        public CustomerController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cust = _context.Customers.ToList();
            return View(cust);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer cust)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(cust);
                int insert = _context.SaveChanges();
                return insert > 0 ? RedirectToAction(nameof(Index)) : View();
            }
            {
                ModelState.AddModelError("", "Failed to save the customer. Please try again");
            }
            return View(cust);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cust = _context.Customers.Find(id);
            if (cust == null)
            {
                return NotFound();
            }
            return PartialView("_Edit", cust);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer cust)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Update(cust);
                int update = _context.SaveChanges();
                return update > 0 ? RedirectToAction(nameof(Index)) : View();

            }
            {
                ModelState.AddModelError("", "Failed to save the customer. Please try again");
            }
            return View(cust);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var cust = _context.Customers.Find(id);
            if (cust == null)
            {
                return NotFound();
            }
            return PartialView("_Detail", cust);
        }
        public IActionResult Delete(int id)
        {
            var cust = _context.Customers.Find(id);
            if (cust == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(cust);
            int delete = _context.SaveChanges();
            return delete > 0 ? RedirectToAction(nameof(Index)) : View();
        }
    }
}
