using MvcProductDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace MvcProductDemo.Controllers
{
    public class ProductPurchaseController : Controller
    {
        private ProductContext db = new ProductContext();
        //private object productsData;
        //private object ProductsData;
        //private object Products;
        // GET: Product
        public ActionResult Index()
        {
                var purchaseData = db.ProductPurchases.Include(x => x.Product);
            
            return View(purchaseData.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPurchase p = db.ProductPurchases.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        public ActionResult Create()
        {
            PopulateProductDropDownList();
            PopulateCustomerDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "ProductPurchaseId")] ProductPurchase p)
        {
            if (ModelState.IsValid)
            {

                //int total = (from d in db.ProductPurchases where d.Product.ProductName == p.Product.ProductName select d.Product.Quantity + d.Product.Price);
                //int total = p.Product.Price * p.Quantity;

                db.ProductPurchases.Add(p);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
                PopulateProductDropDownList(p.ProductId);
                PopulateCustomerDropDownList(p.CustId);
                return View(p);
        }
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPurchase p = db.ProductPurchases.Find(Id);
            if (p == null)
            {
                return HttpNotFound();
            }
            PopulateCustomerDropDownList(p.Product.ProductId);
            PopulateCustomerDropDownList(p.Customer.CustId);
            return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductPurchase p)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            PopulateCustomerDropDownList(p.Product.ProductId);
            PopulateCustomerDropDownList(p.Customer.CustId);
            return View(p);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPurchase p = db.ProductPurchases.Find(id);
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
            ProductPurchase p = db.ProductPurchases.Find(id);
            db.ProductPurchases.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private void PopulateProductDropDownList(object SelectedProduct = null)
        {
            var ProductQuery = from c in db.Products
                                orderby c.ProductName
                                select c;
            ViewBag.ProductId = new SelectList(ProductQuery, "ProductId", "ProductName", SelectedProduct);
        }
        private void PopulateCustomerDropDownList(object SelectedCustomer = null)
        {
            var CustomerQuery = from c in db.Customers
                               orderby c.CustName
                               select c;
            ViewBag.CustId = new SelectList(CustomerQuery, "CustId", "CustName", SelectedCustomer);
        }
    }
}
