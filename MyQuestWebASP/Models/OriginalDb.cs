using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyQuestWebASP.Models
{
    public class Service
    {
        public Service()
        {
            ServiceForOrders = new List<ServiceForOrder>();
        }
        public int ServiceId { get; set; }
        public string TitleService { get; set; }
        public string DescriptionService { get; set; }
        public Decimal CostPerService { get; set; }
        public int CategoryId { get; set; }

        public virtual ICollection<ServiceForOrder> ServiceForOrders { get; set; }

        public Category Category { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string TypeCategory { get; set; }
    }

    public class Order
    {

        public Order()
        {
            ServiceForOrders = new List<ServiceForOrder>();
        }

        public int OrderId { get; set; }
        public string NameClient { get; set; }
        public string PhoneClient { get; set; }
        public decimal PayClient { get; set; }
        public DateTime? DateCreateOrder { get; set; }
        public int StatusId { get; set; }
        public DateTime? DateLastModifOrder { get; set; }

        public virtual ICollection<ServiceForOrder> ServiceForOrders { get; set; }
        public Status Status { get; set; }
    }

    public class Status
    {
        public int StatusId { get; set; }
        public string StatusOrder { get; set; }
    }

    public class ServiceForOrder
    {

        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public int QuantityService { get; set; }

        public Order Order { get; set; }
        public Service Service { get; set; }
    }
}