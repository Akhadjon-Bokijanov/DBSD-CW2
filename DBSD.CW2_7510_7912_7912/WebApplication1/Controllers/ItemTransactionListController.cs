using DBSD_CW2_7510_8775_7912.DAL;
using DBSD_CW2_7510_8775_7912.Models;
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
            var tl = new ItemTransactionListManager();
            return View(tl.GetAll());
        }

        // GET: ItemTransactionList/Details/5
        public ActionResult Details(int id)
        {
            var tl = new ItemTransactionListManager();

            return View(tl.GetOne(id));
        }

        // GET: ItemTransactionList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemTransactionList/Create
        [HttpPost]
        public ActionResult Create(ItemTransactionList collection)
        {
            try
            {
                // TODO: Add insert logic here

                var tl = new ItemTransactionListManager();

                tl.Create(collection);

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
            var tl = new ItemTransactionListManager();

            return View(tl.GetOne(id));
        }

        // POST: ItemTransactionList/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ItemTransactionList collection)
        {
            try
            {
                // TODO: Add update logic here

                var tl = new ItemTransactionListManager();

                tl.Update(id, collection);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw ex;
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
                var tl = new ItemTransactionListManager();
                tl.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
