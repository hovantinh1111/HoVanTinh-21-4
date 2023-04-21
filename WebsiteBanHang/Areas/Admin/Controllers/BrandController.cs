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
    public class BrandController : Controller
    {
        WebsiteBanHangEntities objwebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Brand
        public ActionResult Index(string SearchString, string currentFiler, int? page)
        {

            var lstBrand = new List<Brand>();
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
                lstBrand = objwebsiteBanHangEntities.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = objwebsiteBanHangEntities.Brands.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();



            return View(lstBrand.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Details(int Id)
        {
            var objProducut = objwebsiteBanHangEntities.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProducut);
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objBra)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    if (objBra.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBra.ImageUpload.FileName);
                        string extension = Path.GetExtension(objBra.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objBra.Avartar = fileName;
                        objBra.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objwebsiteBanHangEntities.Brands.Add(objBra);
                    objwebsiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(objBra);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = objwebsiteBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Brand objBra)
        {

            if (objBra.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBra.ImageUpload.FileName);
                string extension = Path.GetExtension(objBra.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objBra.Avartar = fileName;
                objBra.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objwebsiteBanHangEntities.Entry(objBra).State = EntityState.Modified;
            objwebsiteBanHangEntities.SaveChanges();
            return View(objBra);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objwebsiteBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objBran)
        {
            var objBrand = objwebsiteBanHangEntities.Brands.Where(n => n.Id == objBran.Id).FirstOrDefault();
            objwebsiteBanHangEntities.Brands.Remove(objBrand);
            objwebsiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}