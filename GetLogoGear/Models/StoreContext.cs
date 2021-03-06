﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GetLogoGear.Models
{
    public class StoreContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public StoreContext() : base("name=StoreContext")
        {
        }

        public System.Data.Entity.DbSet<GetLogoGear.Models.Color> Colors { get; set; }

        public System.Data.Entity.DbSet<GetLogoGear.Models.BaseItem> BaseItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseItem>()
                .HasMany(up => up.Colors)
                .WithMany(color => color.BaseItems)
                .Map(mc =>
                {
                    mc.ToTable("T_BaseItem_Color");
                    mc.MapLeftKey("BaseItemID");
                    mc.MapRightKey("ColorID");
                }
                
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
