using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyQuestWebASP.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public string UserLogin { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ServiceForOrder> ServicesForOrder { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<Status>().HasKey(x => x.StatusId);
            modelBuilder.Entity<Category>().HasKey(x => x.CategoryId);
            modelBuilder.Entity<Service>().HasKey(x => x.ServiceId);
            modelBuilder.Entity<Order>().HasKey(x => x.OrderId);
            modelBuilder.Entity<ServiceForOrder>().HasKey(x => new { x.OrderId, x.ServiceId });

            modelBuilder.Entity<Order>().HasMany(x => x.ServiceForOrders).WithRequired(x => x.Order).HasForeignKey(x => x.OrderId).WillCascadeOnDelete();

            modelBuilder.Entity<Service>().HasMany(x=>x.ServiceForOrders).WithRequired(x=>x.Service).HasForeignKey(x=>x.ServiceId).WillCascadeOnDelete();


            modelBuilder.Entity<ServiceForOrder>().HasKey(t => new { t.OrderId, t.ServiceId });
            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<MyQuestWebASP.Models.InfoOrder> InfoOrders { get; set; }

        public System.Data.Entity.DbSet<MyQuestWebASP.Models.OrderModel> OrderModels { get; set; }
    }
}