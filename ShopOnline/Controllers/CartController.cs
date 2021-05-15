using Model.Dao;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using Model.EF;

namespace ShopOnline.Controllers
{
    public class CartController : Controller
    {
        private const string CART_SEESION = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CART_SEESION];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;

            }
            return View(list);
        }
        public JsonResult DeleteAll()
        {
            Session[CART_SEESION] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CART_SEESION];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CART_SEESION] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CART_SEESION];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CART_SEESION] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(long productID,int quantity)
        {
            var product = new ProductDao().ViewDetail(productID);
            var cart = Session[CART_SEESION];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productID))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productID)
                        {
                            item.Quantity += quantity;
                        }

                    }
                }
                else
                {
                    //create new obj cart item
                    CartItem item = new CartItem
                    {
                        Product = product,
                        Quantity = quantity
                    };
                    list.Add(item);
                }
                Session[CART_SEESION] = list;
            }
            else
            {
                //create new obj cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //assign into session
                Session[CART_SEESION] = list;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Payment()
        {
            var cart = Session[CART_SEESION];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            ViewBag.ListCart = list;
            return View();
        }
        [HttpPost]
        public ActionResult Payment(OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                Random customerid = new Random();
                var order = new Order();
                order.CreateDate = DateTime.Now;
                order.CustomerID = customerid.Next(1000, 9999);
                order.ShipAddress = orderModel.ShipAddress;
                order.ShipMobile = orderModel.ShipPhone;
                order.ShipName = orderModel.ShipName;
                order.ShipEmail = orderModel.ShipEmail;
                order.TotalPrice = (int)Session["TotalPrice"];
                try
                {
                    var id = new OrderDao().Insert(order);
                    var cart = (List<CartItem>)Session[CART_SEESION];
                    var detailDao = new OrderDetailDao();
                    foreach (var item in cart)
                    {
                        var orderDetail = new OrderDetail();
                        orderDetail.ProductID = item.Product.ID;
                        orderDetail.OrderID = id;
                        orderDetail.Price = item.Product.PromotionPrice.HasValue ? item.Product.PromotionPrice : item.Product.Price;
                        orderDetail.Quantity = item.Quantity;
                        detailDao.Insert(orderDetail);
                    }
                    return Redirect("/hoan-thanh/");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                ModelState.AddModelError("", "Order chưa thành công.");
                return RedirectToAction("Payment");
            }

              

        }
        public ActionResult Success()
        {
            Session.Remove("TotalPrice");
            Session.Remove(CART_SEESION);
            return View();
        }
    }
}