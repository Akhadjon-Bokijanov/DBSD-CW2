using DBSD_CW2_7510_8775_7912.DAL;
using DBSD_CW2_7510_8775_7912.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBSD_CW2_7510_8775_7912.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            var s = new SupplierManager();
            return View(s.GetAll());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            var s = new SupplierManager();

            return View(s.GetOne(id));
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(Supplier collection)
        {
            try
            {
                // TODO: Add insert logic here

                var s = new SupplierManager();
                s.Create(collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            var s = new SupplierManager();
            return View(s.GetOne(id));
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Supplier collection)
        {
            try
            {
                // TODO: Add update logic here
                var s = new SupplierManager();

                s.Update(id, collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            var s = new SupplierManager();

            return View(s.GetOne(id));
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                var s = new SupplierManager();

                s.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
