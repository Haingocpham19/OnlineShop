using Model.Dao;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;

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
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
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
    }
}