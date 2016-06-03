using MvcProductDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MvcProductDemo.Controllers
{
    public class ProductController : Controller
    {
        private ProductContext db = new ProductContext();
        //private object productsData;
        //private object ProductsData;
        //private object Products;
        // GET: Product
        public ActionResult Index(string sortOrder,string SortParam,string SearchString,string CurrentFilter,int? page)
        {
            //var ViewModel = new ProductInsexData();
            //     var products
            ViewBag.CurrentSort = sortOrder;
            if(SearchString != null)
                {
                page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var productsData = db.Products.Include(x => x.Category);
            //var Products;
            ViewBag.Sort = String.IsNullOrEmpty(sortOrder) ? "desc" : "";
            //ViewBag.SortParam = String.IsNullOrEmpty(SortParam) ? "Product Name" : ViewBag.SortParam;
                
                if(!String.IsNullOrEmpty(SearchString))
            {
                productsData = productsData.Where(p => p.ProductName.Contains(SearchString) || p.Category.CategoryName.Contains(SearchString));
            }
            switch (sortOrder)
            {
                case "desc":
                        if (SortParam == "Category Name")
                        {
                           productsData = productsData.OrderByDescending(p => p.Category.CategoryName);
                        }
                        else if(SortParam == "Price")
                        {
                            productsData = productsData.OrderByDescending(p => p.Price);
                        }
                        else
                        {
                            
                          productsData = productsData.OrderByDescending(p => p.ProductName);
                        }
                        break;
                default:
                        if (SortParam == "Category Name")
                        {
                           productsData = productsData.OrderBy(p => p.Category.CategoryName);
                        }
                        else if (SortParam == "Price")
                        {
                            productsData = productsData.OrderBy(p => p.Price);
                        }
                        else
                        {
                            
                          productsData = productsData.OrderBy(p => p.ProductName);
                        }
                        
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(productsData.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Details(int? id)
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
        public ActionResult Create()
        {
            PopulateCategoryDropDownList();
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
                PopulateCategoryDropDownList(p.CategoryId);
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
            PopulateCategoryDropDownList(p.CategoryId);
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
            PopulateCategoryDropDownList(p.CategoryId);
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
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product p = db.Products.Find(id);
            db.Products.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private void PopulateCategoryDropDownList(object SelectedCategory = null)
        {
            var CategoryQuery = from c in db.Categories
                                orderby c.CategoryName
                                select c;
            ViewBag.CategoryId = new SelectList(CategoryQuery, "CategoryId", "CategoryName", SelectedCategory);
        }
    }

}