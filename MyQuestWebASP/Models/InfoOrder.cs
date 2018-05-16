using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using  System.ComponentModel.DataAnnotations.Schema;

namespace MyQuestWebASP.Models
{
    public class InfoOrder
    {
        [Display(Name = "Номер заказа:")]
        public int InfoOrderId { get; set; }
        [Display(Name = "Фамилия и имя клиента:")]
        public string InfoOrderNameClient { get; set; }
        [Display(Name = "Номер телефона клиента:")]
        public string InfoOrderPhoneNumb { get; set; }
        [Display(Name = "Оплаченная сумма клиентом:")]
        public decimal InfoOrderPayClient { get; set; }
        [Display(Name = "Дата и время создания заказа:")]
        public DateTime InfoOrderDateCreateOrder { get; set; }
        [Display(Name = "Стастус заказа:")]
        public string InfoOrderStatus { get; set; }
        [Display(Name = "Дата и время изменения заказа:")]
        public DateTime InfoOrderDateLastMod { get; set; }
    }
}