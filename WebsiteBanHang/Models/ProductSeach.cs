using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Models
{
    public class ProductSeach
    {
        WebsiteBanHangEntities objwebsiteBanHangEntities = new WebsiteBanHangEntities();
        public List<Product> SearchByKey(string key)
        {
            return objwebsiteBanHangEntities.Products.SqlQuery("Select * From Product Where Name like '%" + key + "%'").ToList();
        }
    }
}