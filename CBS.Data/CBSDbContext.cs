using CBS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBS.Data
{
    public class CBSDbContext:DbContext
    {
        public CBSDbContext(DbContextOptions<CBSDbContext> options) : base(options)
        {
        }       
        public DbSet<Clinic> Clinic { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Appointment> Appointment { get; set; } 
    }
}
