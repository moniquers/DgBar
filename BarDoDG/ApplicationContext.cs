﻿using BarDoDG.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarDoDG
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(t => t.Id);

            modelBuilder.Entity<Order>().HasKey(t => t.Id);
            modelBuilder.Entity<Order>().HasMany(t => t.Items)
                .WithOne(t => t.Order);
          
            modelBuilder.Entity<OrderItem>().HasKey(t => t.Id);
            modelBuilder.Entity<OrderItem>().HasOne(t => t.Order);
            modelBuilder.Entity<OrderItem>().HasOne(t => t.Product);
        }
    }
}
