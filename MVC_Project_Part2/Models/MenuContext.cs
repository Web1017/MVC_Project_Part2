namespace MVC_Project_Part2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MenuContext : DbContext
    {
        public MenuContext()
            : base("name=MenuConnection")
        {
        }

        public virtual DbSet<Menu_List> Menu_Lists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu_List>()
                .Property(e => e.ShortDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Menu_List>()
                .Property(e => e.LongDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Menu_List>()
                .Property(e => e.IconImage)
                .IsUnicode(false);

            modelBuilder.Entity<Menu_List>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<Menu_List>()
                .Property(e => e.ImagePath)
                .IsUnicode(false);
        }
    }
}
