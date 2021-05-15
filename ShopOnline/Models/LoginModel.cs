using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name ="Tên đăng nhập")]
        [Required(ErrorMessage = "Mời nhập user name")]
        public string UserName { set; get; }
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Mời nhập password")]
        public string Password { set; get; }
        [Display(Name = "Ghi nhớ!")]
        public bool RememberMe { set; get; }
    }
}