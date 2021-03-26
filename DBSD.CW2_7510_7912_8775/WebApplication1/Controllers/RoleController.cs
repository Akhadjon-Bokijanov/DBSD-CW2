using DBSD_CW2_7510_8775_7912.DAL;
using DBSD_CW2_7510_8775_7912.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBSD_CW2_7510_8775_7912.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {

            var m = new RoleManager();

            return View(m.GetAll());
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            var m = new RoleManager();
            return View(m.GetOne(id));
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(Role collection)
        {
            try
            {
                // TODO: Add insert logic here

                var m = new RoleManager();
                m.Create(collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            var m = new RoleManager();
            return View(m.GetOne(id));
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Role collection)
        {
            try
            {
                // TODO: Add update logic here

                var m = new RoleManager();
                m.Update(id, collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            var m = new RoleManager();
            return View(m.GetOne(id));
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                var m = new RoleManager();
                m.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
