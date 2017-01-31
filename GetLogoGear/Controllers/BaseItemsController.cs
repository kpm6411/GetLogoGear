using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GetLogoGear.Models;
using System.IO;
using GetLogoGear.ViewModels;

namespace GetLogoGear.Controllers
{
    public class BaseItemsController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: BaseItems
        public ActionResult Index()
        {
            return View(db.BaseItems.ToList());
        }

        // GET: BaseItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaseItem baseItem = db.BaseItems.Find(id);
            if (baseItem == null)
            {
                return HttpNotFound();
            }
            return View(baseItem);
        }

        // GET: BaseItems/Create
        public ActionResult Create()
        {
            var baseItem = new BaseItem();
            baseItem.Colors = new List<Color>();
            PopulateColorData(baseItem);
            return View();
        }


        // POST: BaseItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,HasSizes,Price,Image, Colors")] BaseItem baseItem, string[] selectedColors, HttpPostedFileBase upload)
        {
            try
            {
                if (selectedColors != null)
                {
                    baseItem.Colors = new List<Color>();
                    foreach (var color in selectedColors)
                    {
                        var colorToAdd = db.Colors.Find(int.Parse(color));
                        baseItem.Colors.Add(colorToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    if (upload != null)
                    {
                        string pic = System.IO.Path.GetFileName(upload.FileName);
                        //string pic = "baseitem" + baseItem.ItemID;
                        string path = System.IO.Path.Combine(Server.MapPath("~/Images/BaseItems"), pic);
                        upload.SaveAs(path);
                        baseItem.Image = "Images/BaseItems/" + upload.FileName;
                    }

                    db.BaseItems.Add(baseItem);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateColorData(baseItem);
            return View(baseItem);
        }

        // GET: BaseItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaseItem baseItem = db.BaseItems.Find(id);
            if (baseItem == null)
            {
                return HttpNotFound();
            }
            return View(baseItem);
        }

        // POST: BaseItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,Name,Description,Sizes,Price,Image")] BaseItem baseItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baseItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(baseItem);
        }

        private void PopulateColorData(BaseItem baseItem)
        {
            var allColors = db.Colors;
            var itemColors = new HashSet<int>(baseItem.Colors.Select(c => c.ColorID));
            var viewModel = new List<AssignedColorData>();
            foreach (var color in allColors)
            {
                viewModel.Add(new AssignedColorData 
                {
                    ColorID = color.ColorID,
                    Name = color.Name,
                    Assigned = itemColors.Contains(color.ColorID)
                });
            }
            ViewBag.Colors = viewModel;
        }

        // GET: BaseItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaseItem baseItem = db.BaseItems.Find(id);
            if (baseItem == null)
            {
                return HttpNotFound();
            }
            return View(baseItem);
        }

        // POST: BaseItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaseItem baseItem = db.BaseItems.Find(id);
            db.BaseItems.Remove(baseItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
