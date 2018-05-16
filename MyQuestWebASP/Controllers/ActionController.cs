using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Security;
using MyQuestWebASP.Models;

namespace MyQuestWebASP.Controllers
{
    public class ActionController : Controller
    {
        ApplicationDbContext db = ApplicationDbContext.Create();
        //GET
        [Authorize(Roles = "Admin")]
        public ActionResult AllInfo()
        {
            //var order = db.Orders;
            List<InfoOrder> infoOrders = new List<InfoOrder>();

            //infoOrders = (from Order in db.Orders
            //    join Status in db.Statuses on Order.StatusId equals Status.StatusId
            //    select new InfoOrder
            //    {
            //        InfoOrderId=Order.OrderId,
            //        InfoOrderNameClient=Order.NameClient,
            //        InfoOrderPhoneNumb=Order.PhoneClient,
            //        InfoOrderPayClient=Order.PayClient,
            //        InfoOrderDateCreateOrder=Order.DateCreateOrder,
            //        InfoOrderStatus=Status.StatusOrder,
            //        InfoOrderDateLastMod=Order.DateLastModifOrder
            //    }).ToList();

                       ViewBag.Orders = infoOrders;

            var order = db.Orders.Include(x => x.Status).ToList();


            return View(order);
        }
    }
}
