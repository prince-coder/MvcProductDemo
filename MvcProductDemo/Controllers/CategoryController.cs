using MvcProductDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcProductDemo.Controllers
{
    public class CategoryController : Controller
    {
        private ProductContext db = new ProductContext();
        // GET: Category
        public ActionResult Index()
        {

            return View(db.Categories.ToList());
        }
        public ActionResult Details([Bind(Include ="CategoryName")] int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category c = db.Categories.Find(id);
            if(c==null)
            {
                return HttpNotFound();
            }
            return View(c);
        }
        public ActionResult Create()
        {
            return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind (Exclude ="CategoryId")] Category c)
        {
            if(ModelState.IsValid)
            {
                db.Categories.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category c = db.Categories.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category c)
        {
            if(ModelState.IsValid)
            {
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category c = db.Categories.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category c = db.Categories.Find(id);
            db.Categories.Remove(c);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}