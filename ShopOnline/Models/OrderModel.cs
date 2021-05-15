using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class OrderModel
    {
        [Key]
        public long ID { set; get; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage ="Chưa nhập tên")]
        public string ShipName { set; get; }

        [Required(ErrorMessage ="Chưa nhập địa chỉ")]
        [Display(Name = "Địa chỉ")]
        public string ShipAddress { set; get; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Yêu cầu nhập Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string ShipEmail { set; get; }

        [Display(Name = "Số điện thoại")]
        public string ShipPhone { set; get; }
    }
}