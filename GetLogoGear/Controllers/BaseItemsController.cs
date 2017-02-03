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
using System.Data.Entity.Infrastructure;

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
        public ActionResult Create([Bind(Include = "ItemID,Name,Description,HasSizes,Price,Image, Colors")] BaseItem baseItem, string[] selectedColors, HttpPostedFileBase upload)
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
                        //string[] arr = System.IO.Path.GetFileName(upload.FileName).Split('.');
                        //string pic = baseItem.ItemID.ToString() + "." + arr[1];
                        string pic = System.IO.Path.GetFileName(upload.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Images/BaseItems"), pic);
                        upload.SaveAs(path);
                        baseItem.Image = "Images/BaseItems/" + pic;
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
            BaseItem baseItem = db.BaseItems
                .Include(i => i.Colors)
                .Where(i => i.ItemID == id)
                .Single();
            PopulateColorData(baseItem);
            if (baseItem == null)
            {
                return HttpNotFound();
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

        // POST: BaseItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedColors, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var baseItemToUpdate = db.BaseItems
                .Include(i => i.Colors)
                .Where(i => i.ItemID == id)
                .Single();

            if (TryUpdateModel(baseItemToUpdate, "",
                new string[] { "Name", "Description", "HasSizes", "Price", "Colors" }))
            {
                try
                {
                    UpdateItemColors(selectedColors, baseItemToUpdate);
                    if (upload != null)
                    {
                        string oldPath = System.IO.Path.Combine(Server.MapPath("~/"), baseItemToUpdate.Image);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                        //string[] arr = System.IO.Path.GetFileName(upload.FileName).Split('.');
                        //string pic = baseItem.ItemID.ToString() + "." + arr[1];
                        string pic = System.IO.Path.GetFileName(upload.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Images/BaseItems"), pic);
                        upload.SaveAs(path);
                        baseItemToUpdate.Image = "Images/BaseItems/" + pic;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateColorData(baseItemToUpdate);
            return View(baseItemToUpdate);
        }

        private void UpdateItemColors(string[] selectedColors, BaseItem baseItemToUpdate)
        {
            if (selectedColors == null)
            {
                baseItemToUpdate.Colors = new List<Color>();
                return;
            }

            var selectedColorsHS = new HashSet<string>(selectedColors);
            var baseItemColors = new HashSet<int>
                (baseItemToUpdate.Colors.Select(c => c.ColorID));
            foreach (var color in db.Colors)
            {
                if (selectedColorsHS.Contains(color.ColorID.ToString()))
                {
                    if (!baseItemColors.Contains(color.ColorID))
                    {
                        baseItemToUpdate.Colors.Add(color);
                    }
                }
                else
                {
                    if (baseItemColors.Contains(color.ColorID))
                    {
                        baseItemToUpdate.Colors.Remove(color);
                    }
                }
            }
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
            BaseItem baseItem = db.BaseItems
                .Include(i => i.Colors)
                .Where(d => d.ItemID == id)
                .Single();
            string imgPath = System.IO.Path.Combine(Server.MapPath("~/"), baseItem.Image);
            if (System.IO.File.Exists(imgPath))
            {
                System.IO.File.Delete(imgPath);
            }
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
