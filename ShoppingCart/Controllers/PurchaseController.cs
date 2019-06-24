using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.DB;
using ShoppingCart.filter;
namespace ShoppingCart.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Index()
        {
            return View();
        }
        [SessionAuthorize]
        public ActionResult MyPurchase(int userId)
        {
            string productlist = Session["addlist"] as string;
            int count = (int)Session["count"];
            List<Purchase> l = PurchaseData.GetListPurchase(userId);
            
            ViewData["list"] = l;
            ViewData["addlist"] = productlist;
            ViewData["count"] = count;
            return View();
        }
    }
}