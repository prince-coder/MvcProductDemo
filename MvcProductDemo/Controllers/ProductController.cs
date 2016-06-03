using MvcProductDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcProductDemo.Controllers
{
    public class ProductController : Controller
    {
        private ProductContext db = new ProductContext();
        //private object productsData;
        //private object ProductsData;

        // GET: Product
        public ActionResult Index()
        {
       //     var products;
        var productsData = db.Products.Include(x => x.Categories);

            return View(productsData.ToList());
        }
        public ActionResult Details(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product p = db.Products.Find(id);
            if(p==null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "ProductId")] Product p)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product p = db.Products.Find(Id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product p = db.Products.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product p = db.Products.Find(id);
            db.Products.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
        
}