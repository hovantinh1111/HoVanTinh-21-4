using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public partial class ProductMasterData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avartar { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string ShortDes { get; set; }
        public string FullDiceDiscount { get; set; }
        public string FullDesciption { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> PiceDiscount { get; set; }
        public Nullable<int> Typeld { get; set; }
        public string Slug { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }

    }
}