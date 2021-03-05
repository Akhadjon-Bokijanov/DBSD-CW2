using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBSD_CW2_7510_8775_7912.Controllers
{
    public class ItemTransactionListController : Controller
    {
        // GET: ItemTransactionList
        public ActionResult Index()
        {
            return View();
        }

        // GET: ItemTransactionList/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemTransactionList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemTransactionList/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemTransactionList/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ItemTransactionList/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemTransactionList/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ItemTransactionList/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
