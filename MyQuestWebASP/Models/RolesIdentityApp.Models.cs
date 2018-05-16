using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyQuestWebASP.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            if (context.Orders.Any() || context.Services.Any() || context.ServicesForOrder.Any() ||
                context.Statuses.Any() || context.Categories.Any())
            {
                return; // данные есть
            }

            var stat1 = new Status() { StatusOrder = "processing" };
            var stat2 = new Status() { StatusOrder = "done" };

            var cat1 = new Category() { TypeCategory = "Печать" };
            var cat2 = new Category() { TypeCategory = "Дизайн" };
            var cat3 = new Category() { TypeCategory = "Продажа" };

            var serv1 = new Service()
            {
                TitleService = "Печать формата А3",
                DescriptionService = "Печать формата А3 размер: 297×420 мм",
                CostPerService = 200,
                Category = cat1
            };

            var serv2 = new Service()
            {
                TitleService = "Печать формата А4",
                DescriptionService = "Печать формата А4 размер: 210×297 мм",
                CostPerService = 150,
                Category = cat1
            };

            var serv3 = new Service()
            {
                TitleService = "Создание лого для визитки",
                DescriptionService = "Создание логотипа для оформления визитных карточек",
                CostPerService = 25,
                Category = cat2
            };

            var ord1 = new Order()
            {
                NameClient = "Николай Н.",
                Status = stat2,
                PhoneClient = "89991501010",
                DateCreateOrder = DateTime.Now,
                DateLastModifOrder = DateTime.Now,
                PayClient = 200
            };

            var ord2 = new Order()
            {
                NameClient = "Асханов У.",
                Status = stat1,
                PhoneClient = "89991501515",
                DateCreateOrder = DateTime.Now,
                DateLastModifOrder = DateTime.Now,
                PayClient = 50
            };

            context.Statuses.Add(stat1);
            context.Statuses.Add(stat2);
            context.SaveChanges();

            context.Categories.Add(cat1);
            context.Categories.Add(cat2);
            context.Categories.Add(cat3);
            context.SaveChanges();

            context.Orders.Add(ord1);
            context.Orders.Add(ord2);
            context.SaveChanges();

            context.Services.Add(serv1);
            context.Services.Add(serv2);
            context.Services.Add(serv3);
            context.SaveChanges();

            ord1.ServiceForOrders.Add(new ServiceForOrder() { Service = serv1, QuantityService = 1 });
            ord2.ServiceForOrders.Add(new ServiceForOrder() { Service = serv3, QuantityService = 2 });
            context.SaveChanges();




            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "Admin" };
            var role2 = new IdentityRole { Name = "User" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser { UserName = "AdminForMyTest" };
            string password = "Qwerty1234@";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);

            }


            base.Seed(context);
        }


    }
    //public class OriginDbInitializer : DropCreateDatabaseAlways<OriginDbContext>
    //{
    //    protected override void Seed(OriginDbContext db)
    //    {
    //        var status = new ApplicationDbContext();

    //        db.Statuses.Add(new Status { StatusOrder = "processing" });
    //        db.Statuses.Add(new Status { StatusOrder = "done" });
    //        db.Orders.Add(new Order
    //        {
    //            NameClient = "Николай Н.",
    //            StatusId = 2,
    //            PhoneClient = "89991501010",
    //            DateCreateOrder = DateTime.Now,
    //            DateLastModifOrder = DateTime.Now,
    //            PayClient = 200
    //        });
    //        db.SaveChanges();

    //        base.Seed(db);
    //    }
    //}
}