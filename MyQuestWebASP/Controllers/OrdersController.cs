using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MyQuestWebASP.Models;

namespace MyQuestWebASP.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Status);
            return View(orders.ToList());
        }

        [HttpGet]
        public ActionResult CreateOrder()
        {
            var ordModel= new OrderModel();
SelectList category = new SelectList(db.Categories, "CategoryId", "TypeCategory");
            ViewBag.Category = category;
            return View(ordModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(OrderModel ordModel)
        {
            if (ModelState.IsValid)
            {
                Order ord = new Order()
                {
                    
                    NameClient = ordModel.NameClient,
                    PhoneClient = ordModel.PhoneNumbClient,
                    PayClient = ordModel.ClientPayForOrder,
                    DateCreateOrder = getDateTime(),
                    StatusId = ordModel.StatusId,
                    DateLastModifOrder = getDateTime()
                };
                db.Orders.Add(ord);
               var lastInsertId = ord.OrderId;
                db.SaveChanges();
                ServiceForOrder servForOrder = new ServiceForOrder()
                {
                    OrderId = ord.OrderId,
                    ServiceId = ordModel.ServiceId,
                        //Convert.ToInt32(ordModel.services.First()),
                    QuantityService = ordModel.QuantityItem
                };

                
                db.ServicesForOrder.Add(servForOrder);
                db.SaveChanges();

                return Redirect("Index");
            }


            return HttpNotFound();
        }


        public ActionResult IdexSearch(string statusOrder, string searchString)
        {
            var StatusList = new List<string>();

            var StatusQry = db.Orders.Include(t => t.Status).OrderBy(t => t.Status.StatusOrder).Select(t => t.Status.StatusOrder);

            StatusList.AddRange(StatusQry.Distinct());

            ViewBag.statusOrder = new SelectList(StatusList, "done");
            
            var order = db.Orders.Select(t => t).Include(s => s.Status);

            if (!String.IsNullOrEmpty(searchString))
            {
                order = order.Where(s => s.NameClient.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(statusOrder))
            {
                order = order.Where(t => t.Status.StatusOrder == statusOrder);
            }

            return View(order);
            //var orders = db.Orders.Include(o => o.Statuses);
            //return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = db.Orders.Include(x => x.Status).First(x => x.OrderId == id);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusOrder");
            return View();
        }

        private DateTime getDateTime()
        {
            return DateTime.Now;
        }

        // POST: Orders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,NameClient,PhoneClient,PayClient,DateCreateOrder,StatusId,DateLastModifOrder")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.DateCreateOrder = getDateTime();
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("AddService");
            }

            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusOrder", order.StatusId);
            return View(order);
        }

        public ActionResult AddService(Order order)
        {


            return View();
        }


        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusOrder", order.StatusId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,NameClient,PhoneClient,PayClient,DateCreateOrder,StatusId,DateLastModifOrder")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.DateCreateOrder = db.Orders.Where(t => t.OrderId == order.OrderId)
                    .Select(t => t.DateCreateOrder.Value).First();
                order.DateLastModifOrder = getDateTime();
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusOrder", order.StatusId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
