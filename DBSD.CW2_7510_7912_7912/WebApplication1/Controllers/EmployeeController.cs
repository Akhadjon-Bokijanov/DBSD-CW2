using DBSD_CW2_7510_8775_7912.DAL;
using DBSD_CW2_7510_8775_7912.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBSD_CW2_7510_8775_7912.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            var e = new EmployeeManager();
            return View(e.GetAll());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var e = new EmployeeManager();

            return View(e.GetOne(id));
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var r = new RoleManager();

            ViewBag.Roles = new SelectList(r.GetAll(), "RoleId", "Name");

            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee collection)
        {
            try
            {
                // TODO: Add insert logic here

                var e = new EmployeeManager();

                e.Create(collection);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw ex;
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var e = new EmployeeManager();
            var r = new RoleManager();
            ViewBag.Roles = new SelectList(r.GetAll(), "RoleId", "Name");

            return View(e.GetOne(id));
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee collection)
        {
            try
            {
                // TODO: Add update logic here

                var e = new EmployeeManager();

                e.Update(id, collection);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw ex;
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            var e = new EmployeeManager();
            return View(e.GetOne(id));
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                var e = new EmployeeManager();
                e.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
