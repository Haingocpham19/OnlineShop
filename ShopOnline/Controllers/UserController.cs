using BotDetect.Web.Mvc;
using Model.Dao;
using Model.EF;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register ()
        {
            return View();
        }
        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "registerCapcha", "Sai mã Capcha!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
                }else if(dao.CheckEmail(model.Email)){
                    ModelState.AddModelError("", "Email đã tồn tại.");
                }
                else
                {
                    var user = new User();
                    user.Name = model.Name;
                    user.Password = model.Password;
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.CreateDate = DateTime.Now;
                    user.Status = true;
                    var result = dao.Insert(user);
                    if (result>0)
                    {
                        ViewBag.Success = "Đăng ký thành công.";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");

                    }
                }
            }
            MvcCaptcha.ResetCaptcha("registerCapcha");
            return View(model);
        }
    }
}