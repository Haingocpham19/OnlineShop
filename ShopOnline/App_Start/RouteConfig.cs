using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
           name: "Product Show All",
           url: "san-pham",
           defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
           namespaces: new[] { "ShopOnline.Controllers" }
           );
            routes.MapRoute(
              name: "News Detail",
              url: "tin-tuc/{metatitle}-{id}",
              defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
              namespaces: new[] { "ShopOnline.Controllers" }
              );

            routes.MapRoute(
           name: "Login",
           url: "dang-nhap",
           defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
           namespaces: new[] { "ShopOnline.Controllers" }
           );


           routes.MapRoute(
           name: "Register",
           url: "dang-ky-thanh-vien",
           defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
           namespaces: new[] { "ShopOnline.Controllers" }
           );

            routes.MapRoute(
           name: "Payment Success",
           url: "hoan-thanh",
           defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
           namespaces: new[] { "ShopOnline.Controllers" }
           );

            routes.MapRoute(
            name: "Product",
            url: "san-pham/{metatitle}-{cateID}",
            defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
            namespaces: new[] { "ShopOnline.Controllers" }
            );

            routes.MapRoute(
            name: "Product Detail",
            url: "chi-tiet/{metatitle}-{id}",
            defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
            namespaces: new[] { "ShopOnline.Controllers" }
            );

            routes.MapRoute(
            name: "Add Cart",
            url: "them-gio-hang",
            defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
            namespaces: new[] { "ShopOnline.Controllers" }
            );

            routes.MapRoute(
            name: "Cart",
            url: "gio-hang",
            defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "ShopOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );


            routes.MapRoute(
                name: "News",
                url: "tin-tuc",
                defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Index",
                url: "{controller}/{action}",
                defaults: new { controller = "Iframe", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );
        }
    }
}
