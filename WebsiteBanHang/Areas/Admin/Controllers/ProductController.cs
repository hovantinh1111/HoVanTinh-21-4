using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using static WebsiteBanHang.Commom;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebsiteBanHangEntities objwebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Product

        public ActionResult Index(string SearchString, string currentFiler, int? page)
        {

            var lstProduct = new List<Product>();
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
                lstProduct = objwebsiteBanHangEntities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objwebsiteBanHangEntities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();



            return View(lstProduct.ToPagedList(pageNumber, pageSize));
       
        }
        public ActionResult Details(int Id)
        {
            var objProducut = objwebsiteBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProducut);
        }
        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        // tên hình
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        //pjg
                        fileName = fileName + extension;
                        //Tên hình.jpg
                        objProduct.Avartar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objwebsiteBanHangEntities.Products.Add(objProduct);
                    objwebsiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(objProduct);
        }
        void LoadData()
        {
            Commom objcommom = new Commom();
            // lấy dữ liệu danh mục dưới Db
            var lstCat = objwebsiteBanHangEntities.Categories.ToList();
            //Convert sang select list dang value,text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objcommom.ToSelectList(dtCategory, "Id", "Name");

            //lấy dữ liêuj thương hiệu dưới Db
            var lstBrand = objwebsiteBanHangEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //convert sang select list dang value,text
            ViewBag.ListBrand = objcommom.ToSelectList(dtBrand, "Id", "Name");

            //loai san pham
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            //objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);



            objProductType = new ProductType();
            //objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            //convert select list dang value,text
            ViewBag.ProductType = objcommom.ToSelectList(dtProductType, "Id", "Name");
        }
        public ActionResult Delete(int id)
        {
            var objProduct = objwebsiteBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objwebsiteBanHangEntities.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objwebsiteBanHangEntities.Products.Remove(objProduct);
            objwebsiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objwebsiteBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Product objPro)
        {

            if (objPro.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objPro.ImageUpload.FileName);
                string extension = Path.GetExtension(objPro.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objPro.Avartar = fileName;
                objPro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objwebsiteBanHangEntities.Entry(objPro).State = EntityState.Modified;
            objwebsiteBanHangEntities.SaveChanges();
            return View(objPro);
        }

    }
}