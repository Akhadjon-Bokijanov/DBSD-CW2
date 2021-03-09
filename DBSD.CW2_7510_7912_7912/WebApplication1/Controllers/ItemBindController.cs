using DBSD_CW2_7510_8775_7912.DAL;
using DBSD_CW2_7510_8775_7912.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBSD_CW2_7510_8775_7912.Controllers
{
    public class ItemBindController : Controller
    {
        // GET: ItemBind
        public ActionResult Index()
        {
            var i = new ItemBindManager();
            return View(i.GetAll());
        }

        // GET: ItemBind/Details/5
        public ActionResult Details(int id)
        {
            var i = new ItemBindManager();

            return View(i.GetOne(id));
        }

        // GET: ItemBind/Create
        public ActionResult Create()
        {
            var i = new ItemManager();

            ViewBag.Items = new SelectList(i.GetAll(new ItemFilter() { 
                SortIndex=2, 
                SortCase="DESC" 
            }), "ItemId", "LocalName");

            return View();
        }

        // POST: ItemBind/Create
        [HttpPost]
        public ActionResult Create(ItemBind collection)
        {
            try
            {
                // TODO: Add insert logic here
                var i = new ItemBindManager();
                i.Create(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemBind/Edit/5
        public ActionResult Edit(int id)
        {
            var i = new ItemBindManager();

            var it = new ItemManager();

            ViewBag.Items = new SelectList(it.GetAll(new ItemFilter()
            {
                SortIndex = 2,
                SortCase = "DESC"
            }), "ItemId", "LocalName");

            return View(i.GetOne(id));
        }

        // POST: ItemBind/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ItemBind collection)
        {
            try
            {
                // TODO: Add update logic here

                var i = new ItemBindManager();
                i.Update(id, collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemBind/Delete/5
        public ActionResult Delete(int id)
        {
            var i = new ItemBindManager();
            
            return View(i.GetOne(id));
        }

        // POST: ItemBind/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var i = new ItemBindManager();
                i.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
