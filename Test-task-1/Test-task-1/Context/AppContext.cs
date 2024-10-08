﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_task_1.Model;

namespace Test_task_1
{
    public partial class AppContext : DbContext
    {
        public AppContext()
        {
        }

        public DbSet<FileEntity> Files { get; set; } = null!;

        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-1D3P714\\SQLEXPRESS;Database=HundredFiles;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FileEntity
            modelBuilder.Entity<FileEntity>().HasKey(f => f.Id);
        }
    }
}
