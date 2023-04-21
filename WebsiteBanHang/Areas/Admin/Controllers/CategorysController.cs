using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class CategorysController : Controller
    {
        WebsiteBanHangEntities objwebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Categorys
        public ActionResult Index(string SearchString, string currentFiler, int? page)
        {

            var lstCategorys = new List<Category>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFiler;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstCategorys = objwebsiteBanHangEntities.Categories.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategorys = objwebsiteBanHangEntities.Categories.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            lstCategorys = lstCategorys.OrderByDescending(n => n.Id).ToList();



            return View(lstCategorys.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Details(int Id)
        {
            var objCategorys = objwebsiteBanHangEntities.Categories.Where(n => n.Id == Id).FirstOrDefault();
            return View(objCategorys);
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objCate)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    if (objCate.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCate.ImageUpload.FileName);
                        string extension = Path.GetExtension(objCate.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objCate.Avartar = fileName;
                        objCate.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objwebsiteBanHangEntities.Categories.Add(objCate);
                    objwebsiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(objCate);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategorys = objwebsiteBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategorys);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Category objCate)
        {

            if (objCate.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCate.ImageUpload.FileName);
                string extension = Path.GetExtension(objCate.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objCate.Avartar = fileName;
                objCate.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objwebsiteBanHangEntities.Entry(objCate).State = EntityState.Modified;
            objwebsiteBanHangEntities.SaveChanges();
            return View(objCate);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategorys = objwebsiteBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategorys);
        }
        [HttpPost]
        public ActionResult Delete(Category objCate)
        {
            var objCategorys = objwebsiteBanHangEntities.Categories.Where(n => n.Id == objCate.Id).FirstOrDefault();
            objwebsiteBanHangEntities.Categories.Remove(objCategorys);
            objwebsiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}