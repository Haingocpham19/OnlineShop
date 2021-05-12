using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ShopOnline.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            var model = new ContentDao().ListAllContent();
            return View(model);
        }
        public ActionResult Detail(long id)
        {
            var content = new ContentDao().ViewDetail(id);
            ViewBag.Category = new CategoryDao().ViewDetailCategory(content.CategoryID.Value);
            ViewBag.ListAllContent = new ContentDao().ListAllContent();
            return View(content);
        }
    }
}