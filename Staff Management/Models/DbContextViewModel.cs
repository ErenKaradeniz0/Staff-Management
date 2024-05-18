using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Staff_Management.Models
{
    public class DbContextViewModel : DbContext
    {
        public DbContextViewModel() : base("name=ConnectionStringName")
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<DaysOff> DaysOff { get; set; }
        public DbSet<Assignments> Assignments { get; set; }
    }
}