using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagement.ApplicationDbContext
{
   
    public class RegisterStudent
    {
        private ApplicationDbContext _context;
        public RegisterStudent()
        {
            _context = new ApplicationDbContext();
        }

        public string Register(Student st)
        {
            try
            {
                if (st != null)
                {
                    _context = new ApplicationDbContext();
                    // LINQ to insert the new student into the Students table
                    _context.Students.Add(st);  // Adding the student to the context

                    _context.SaveChanges();     // Save the changes to the database

                    // Return a JSON response indicating success
                    return "Student registered successfully";
                }
                else
                {
                    return "Error in registration. Please try again.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }
    }
}