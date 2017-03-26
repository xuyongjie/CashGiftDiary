using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CashGiftDiary.Web
{
    /// <summary>
    /// The entity framework context
    /// </summary>
    public class DiaryDbContext : DbContext
    {
        public DiaryDbContext(DbContextOptions<DiaryDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var history in this.ChangeTracker.Entries().Where(e => e.Entity is ModelBase && (e.State == EntityState.Added || e.State == EntityState.Modified)).Select(e => e.Entity as ModelBase))
            {
                history.ModifyTime = DateTime.Now;
                if (history.CreateTime == DateTime.MinValue)
                {
                    history.CreateTime = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<CashGiftIn> CashGiftIns { get; set; }
        public DbSet<CashGiftOut> CashGiftOuts { get; set; }
    }
}
