using BotDetect.Web.Mvc;
using Model.Dao;
using Model.EF;
using ShopOnline.Common;
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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                int result = dao.Login(model.UserName, Encryptor.MD5Encryption(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
            }
            else
            {
                ModelState.AddModelError("", "Đăng nhập không đúng");
            }
            return RedirectToAction("Login","User");
        
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
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }
    }
}