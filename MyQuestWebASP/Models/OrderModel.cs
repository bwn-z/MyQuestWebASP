using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyQuestWebASP.Models
{
    public class OrderModel
    {
        //public OrderModel()
        //{
        //    services=new List<Service>();
        //}
        [Display(Name = "Номер заказа:")]
        public int OrderModelId { get; set; }
        [Display(Name = "Фамилия и имя клиента:")]
        public string NameClient { get; set; }
        [Display(Name = "Номер телефона клиента:")]
        public string PhoneNumbClient { get; set; }
        [Display(Name = "Оплаченная сумма клиентом:")]
        public decimal ClientPayForOrder { get; set; }
        [Display(Name = "Дата создания заказа:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = false)]
        public DateTime OrderCreateDateTime { get; set; }
        [Display(Name = "Статус заказа:")]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        [Display(Name = "Дата изменения заказа:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public  DateTime OrderLastModifDateTime { get; set; }

        [Display(Name = "Услуга:")]
        public int ServiceId { get; set; }
        
        //public virtual  ICollection<Service> services { get; set; }
        [Display(Name="Количество:")]
        public int QuantityItem { get; set; }
        public Service Service { get; set; }
    }
}