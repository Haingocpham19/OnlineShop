using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BotDetect.Web.Mvc;

namespace ShopOnline.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { set; get; }
        [Display(Name="Tên đăng nhập")]
        [Required(ErrorMessage ="Yêu cầu cần nhập mật khẩu")]
        public string UserName { set; get; }
        [Display(Name="Mật khẩu")]
        [Required(ErrorMessage ="Yêu cầu nhập mật khẩu")]
        [StringLength(maximumLength:25,MinimumLength=6,ErrorMessage ="Độ dài mật khẩu ít nhất là 6 ký tự.")]
        public string Password { set; get; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password",ErrorMessage ="Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { set; get; }
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
        public string Name { set; get; }
        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Yêu cầu nhập Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { set; get; }
        [Display(Name = "Số điện thoại")]
        public string Phone { set; get; }
    }
}