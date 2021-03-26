using DBSD_CW2_7510_8775_7912.DAL;
using DBSD_CW2_7510_8775_7912.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBSD_CW2_7510_8775_7912.Controllers
{
    public class UnitController : Controller
    {
        // GET: Unit
        public ActionResult Index()
        {
            var m = new UnitManager();
            return View(m.GetAll());
        }

        // GET: Unit/Details/5
        public ActionResult Details(int id)
        {
            var m = new UnitManager();
            return View(m.GetOne(id));
        }

        // GET: Unit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Unit/Create
        [HttpPost]
        public ActionResult Create(Unit collection)
        {
            try
            {
                // TODO: Add insert logic here

                var m = new UnitManager();
                m.Create(collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Unit/Edit/5
        public ActionResult Edit(int id)
        {
            var m = new UnitManager();
            return View(m.GetOne(id));
        }

        // POST: Unit/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Unit collection)
        {
            try
            {
                // TODO: Add update logic here

                var m = new UnitManager();
                m.Update(id, collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Unit/Delete/5
        public ActionResult Delete(int id)
        {
            var m = new UnitManager();
            
            return View(m.GetOne(id));
        }

        // POST: Unit/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var m = new UnitManager();
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
