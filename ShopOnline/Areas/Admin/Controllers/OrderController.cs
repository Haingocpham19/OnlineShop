using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        public ActionResult ViewDetail(long id)
        {
           
            var dao = new OrderDao();
            var model = dao.ViewDetail(id);
            ViewBag.ViewOrderDetail = dao.OrderDetail(id);
            return View(model);
        }
    }
}