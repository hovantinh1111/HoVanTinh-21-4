using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers
{

    public class DetailProductController : Controller
    {
        // GET: DetailProduct
        WebsiteBanHangEntities objwebsiteBanHangEntities = new WebsiteBanHangEntities();
        public ActionResult DetailProduct(int Id)
        {
            Product objProduct = objwebsiteBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}