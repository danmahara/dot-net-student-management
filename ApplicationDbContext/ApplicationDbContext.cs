using StudentManagement.Models;
using System;

using System.Data.Entity;  // EF 6 namespace


namespace StudentManagement.ApplicationDbContext
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection")
        // Using the connection string defined in Web.config
        {


        }

        public DbSet<Student> Students { get; set; }  // Your model class for students
    }
}

