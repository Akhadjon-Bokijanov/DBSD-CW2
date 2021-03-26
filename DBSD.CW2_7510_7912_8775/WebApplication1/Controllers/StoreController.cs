using DBSD_CW2_7510_8775_7912.DAL;
using DBSD_CW2_7510_8775_7912.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBSD_CW2_7510_8775_7912.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            var s = new StoreManager();

            return View(s.GetAll());
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            var s = new StoreManager();

            return View(s.GetOne(id));
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            var e = new EmployeeManager();
            ViewBag.Employees = new SelectList(e.GetAll(), "EmployeeId", "FullName");
            return View();
        }

        // POST: Store/Create
        [HttpPost]
        public ActionResult Create(Store collection)
        {
            try
            {
                // TODO: Add insert logic here

                var s = new StoreManager();

                s.Create(collection);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw ex;
                return View();
            }
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int id)
        {
            var s = new StoreManager();

            var e = new EmployeeManager();
            ViewBag.Employees = new SelectList(e.GetAll(), "EmployeeId", "FullName");

            return View(s.GetOne(id));
        }

        // POST: Store/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Store collection)
        {
            try
            {
                // TODO: Add update logic here

                var s = new StoreManager();

                s.Update(id, collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int id)
        {
            var s = new StoreManager();
            return View(s.GetOne(id));
        }

        // POST: Store/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                var s = new StoreManager();
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
