using DBSD_CW2_7510_8775_7912.DAL;
using DBSD_CW2_7510_8775_7912.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBSD_CW2_7510_8775_7912.Controllers
{
    public class ItemTransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            var t = new ItemTransactionManager();

            return View(t.GetAll());
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int id)
        {
            var t = new ItemTransactionManager();
            return View(t.GetOne(id));
        }

        // GET: Transaction/Create
        public ActionResult Create()
        {
            var e = new EmployeeManager();

            ViewBag.Employees = new SelectList(e.GetAll(), "EmployeeId", "FullName");

            return View();
        }

        // POST: Transaction/Create
        [HttpPost]
        public ActionResult Create(ItemTransaction collection)
        {
            try
            {
                // TODO: Add insert logic here

                var t = new ItemTransactionManager();

                t.Create(collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Transaction/Edit/5
        public ActionResult Edit(int id)
        {
            var t = new ItemTransactionManager();
            var e = new EmployeeManager();

            ViewBag.Employees = new SelectList(e.GetAll(), "EmployeeId", "FullName");
            return View(t.GetOne(id));
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ItemTransaction collection)
        {
            try
            {
                // TODO: Add update logic here

                var t = new ItemTransactionManager();

                t.Update(id, collection);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw ex;
                return View();
            }
        }

        // GET: Transaction/Delete/5
        public ActionResult Delete(int id)
        {
            var t = new ItemTransactionManager();

            return View(t.GetOne(id));
        }

        // POST: Transaction/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                var t = new ItemTransactionManager();
                t.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
