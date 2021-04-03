using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using CsvHelper;
using DBSD_CW2_7510_8775_7912.DAL;
using DBSD_CW2_7510_8775_7912.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DBSD_CW2_7510_8775_7912.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index(ItemFilter filter)
        {
            filter.PageNumber = filter.PageNumber ?? 1;
            filter.PageSize = filter.PageSize ?? 5;

            List<Item> a = new ItemManager().GetAll(filter);

            ViewBag.Filter = filter;

            return View(a);
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {

            var b = new ItemManager();
           
            return View(b.GetOne(id));
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
        public ActionResult Edit(int id, HttpPostedFileBase img)
        {
            var b = new ItemManager();

            var i = new UnitManager();
            ViewBag.Units = new SelectList(i.GetAll(), "UnitId", "Name");

            var sup = new SupplierManager();
            ViewBag.Suppliers = new SelectList(sup.GetAll(), "SupplierId", "Name");

            var st = new StoreManager();
            ViewBag.Stores = new SelectList(st.GetAll(), "StoreId", "Name");

            return View(b.GetOne(id));
        }

        // POST: Item/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Item collection, HttpPostedFileBase img)
        {
            try
            {
                // TODO: Add update logic here

                if (img?.ContentLength > 0)
                {
                    using (var memory = new MemoryStream())
                    {
                        img.InputStream.CopyTo(memory);
                        collection.Image = memory.ToArray();
                    }
                }
                var b = new ItemManager();

                b.Update(id, collection);

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
            var b = new ItemManager();
            return View(b.GetOne(id));
        }

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var b = new ItemManager();
                b.Delete(id);


                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw ex;
                return View();
            }
        }
    
        public FileResult GetItemPhoto(int id)
        {
            var i = new ItemManager();
            Item item = i.GetOne(id);
            if(item != null && item.Image?.Length > 0)
            {
                return File(item.Image, "image/jpeg", item.ItemId + ".jpg");
            }
            return null;
        }

        //Get ?LocalName=Local+name&GlobalName=&ItemUID=&MadeOf=&PageNumber=&PageSize=&SupplierName=&StoreName=&SortIndex=1&SortCase=ASC
        public ActionResult ExportJson(ItemFilter filter)
        {
            var i = new ItemManager();
            var ItemList = i.GetAll(filter);
            var memeory = new MemoryStream();
            var writer = new StreamWriter(memeory);
            var serializer = new JsonSerializer();

            serializer.Serialize(writer, ItemList);
            writer.Flush();
            memeory.Position = 0;
            if (memeory != null)
                return File(memeory, "application/json", $"export_{DateTime.Now}.json");
            return null;
        }


        public ActionResult ExportXml(ItemFilter filter)
        {
            var i = new ItemManager();
            var ItemList = i.GetAll(filter);
            var memeory = new MemoryStream();
            var writer = new StreamWriter(memeory);
            var serializer = new XmlSerializer(typeof(List<Item>));

            serializer.Serialize(writer, ItemList);
            writer.Flush();
            memeory.Position = 0;
            if (memeory != null)
                return File(memeory, "application/xml", $"export_{DateTime.Now}.xml");
            return null;
        }

        public ActionResult ExportCsv(ItemFilter filter)
        {
            var i = new ItemManager();
            var ItemList = i.GetAll(filter);
            var memeory = new MemoryStream();
            var writer = new StreamWriter(memeory);
            var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(ItemList);
            
            writer.Flush();
            memeory.Position = 0;
            if (memeory != null)
                return File(memeory, "application/csv", $"export_{DateTime.Now}.csv");
            return null;
        }

        [HttpPost]
        public ActionResult ImportJson(IFormFile jsonFile)
        {
            try
            {
                if(jsonFile != null)
                {
                    var i = new ItemManager();
                    throw new Exception("Hi");
                    var stream = jsonFile.OpenReadStream();
                    var reader = new StreamReader(stream);
                    var serializer = new JsonSerializer();

                    var items = (List<Item>)serializer.Deserialize(reader, typeof(List<Item>));

                    foreach(Item item in items)
                    {
                        i.Create(item);
                    }

                }
                
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
