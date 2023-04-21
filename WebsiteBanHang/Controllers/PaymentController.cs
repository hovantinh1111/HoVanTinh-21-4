using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class PaymentController : Controller
    {
        WebsiteBanHangEntities objwebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Payment
        public ActionResult Index()
        {

            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objwebsiteBanHangEntities.Orders.Add(objOrder);
                objwebsiteBanHangEntities.SaveChanges();
                int intOrderId = objOrder.Id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.OrderId = intOrderId;
                    obj.Quantity = item.Quantity;
                    obj.Productid = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objwebsiteBanHangEntities.OrderDetails.AddRange(lstOrderDetail);
                objwebsiteBanHangEntities.SaveChanges();
            }
            return View();
        }
    }
}