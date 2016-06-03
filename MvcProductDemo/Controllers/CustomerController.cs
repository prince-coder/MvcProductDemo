using MvcProductDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcProductDemo.Controllers
{
    public class CustomerController : Controller
    {
        private ProductContext db = new ProductContext();
        // GET: Customer
        public ActionResult Index()
        {
           

            return View(db.Customers.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer c = db.Customers.Find(id);
            if (c == null)
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
        public ActionResult Create([Bind(Exclude = "CustId")] Customer c)
        {
            if (ModelState.IsValid)
            {

                //int total = (from d in db.ProductPurchases where d.Product.ProductName == p.Product.ProductName select d.Product.Quantity + d.Product.Price);
                //int total = p.Product.Price * p.Quantity;

                db.Customers.Add(c);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(c);
        }
    }
}