﻿using Microsoft.EntityFrameworkCore;
using MockSchoolManagement.Model;
using MockSchoolManagement.Model.EnumTypes;

namespace MockSchoolManagement.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
