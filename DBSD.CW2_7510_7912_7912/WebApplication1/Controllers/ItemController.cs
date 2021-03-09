using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBSD_CW2_7510_8775_7912.DAL;
using DBSD_CW2_7510_8775_7912.Models;

namespace DBSD_CW2_7510_8775_7912.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index(ItemFilter filter)
        {

            List<Item> a = new ItemManager().GetAll(filter);

            ViewBag.Filter = filter;

            return View(a);
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
           
            Item b = new Item();
           
            return View(b);
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            var i = new UnitManager();
            ViewBag.Units = new SelectList(i.GetAll(), "UnitId", "Name");

            var sup = new SupplierManager();
            ViewBag.Suppliers = new SelectList(sup.GetAll(), "SupplierId", "Name");

            var st = new StoreManager();
            ViewBag.Stores = new SelectList(st.GetAll(), "StoreId", "Name");

            return View();
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Create(Item collection, HttpPostedFileBase img)
        {
            var manager = new ItemManager();
           
            try
            {
                if(img?.ContentLength > 0)
                {
                    using (var memory = new MemoryStream())
                    {
                        img.InputStream.CopyTo(memory);
                        collection.Image = memory.ToArray();
                    }
                }
                // TODO: Add insert logic here

                manager.Create(collection);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw ex; 
                return View(ex);
            }
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int id)
        {
            Item b = new Item();
            var i = new UnitManager();
            ViewBag.Units = new SelectList(i.GetAll(), "UnitId", "Name");

            var sup = new SupplierManager();
            ViewBag.Suppliers = new SelectList(sup.GetAll(), "SupplierId", "Name");

            var st = new StoreManager();
            ViewBag.Stores = new SelectList(st.GetAll(), "StoreId", "Name");

            return View(b);
        }

        // POST: Item/Edit/5
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

        // GET: Item/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Item/Delete/5
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
