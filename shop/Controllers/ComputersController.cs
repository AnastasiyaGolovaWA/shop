using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using shop.Models;

namespace shop.Controllers
{
    public class ComputersController : Controller
    {
        private ComputerContext db = new ComputerContext();
      
        // GET: Computers
        public async Task<ActionResult> Index()
        {
            return View(await db.Computers.ToListAsync());
        }

        // GET: Computers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computer computer = await db.Computers.FindAsync(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // GET: Computers/Create
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Computer computer)
        { 
            db.Computers.Add(computer);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
       
        // POST: Computers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       /* public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Price")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                db.Computers.Add(computer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(computer);
        }
      */
        // GET: Computers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computer computer = await db.Computers.FindAsync(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // POST: Computers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Price")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(computer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(computer);
        }
        [HttpGet]
        public ActionResult AboutCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ComputerSearch(string name)
        {
            var allcomputers = db.Computers.Where(a => a.Name.Contains(name)).ToList();
            if (allcomputers.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(allcomputers);
        }
        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо за покупку!";
        }
        [HttpPost]
        public ActionResult ComputerSearchByPrice(string name, int price)
        {
            var allcomputers = db.Computers.Where(a => a.Name.Contains(name) && a.Price < price).ToList();
            if (allcomputers.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(allcomputers);
        }

        // GET: Computers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computer computer = await db.Computers.FindAsync(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Computer computer = await db.Computers.FindAsync(id);
            db.Computers.Remove(computer);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Partial()
        {
            ViewBag.Message = "Это частичное представление.";
            return PartialView();
        }
    }
}
