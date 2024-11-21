using StudentManagement.ApplicationDbContext;

using StudentManagement.Models;
using System;
using System.Linq;
using StudentManagement.Helper;

using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext.ApplicationDbContext _context;

        //// GET: Student

        public StudentController()
        {
            // Initialize the context here
            _context = new ApplicationDbContext.ApplicationDbContext();
        }

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // POST: Register Student
        [HttpPost]
        public JsonResult Register(Student st)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RegisterStudent studentRegistrar = new RegisterStudent();
                    st.Username = SlugHelper.CreateSlug(st.Name);
                    string result = studentRegistrar.Register(st);
                    if (result == "Student registered successfully"){
                        return Json(new { success = true, message = result });
                    }
                    else
                    {
                        return Json(new { success = false, message = result });
                    }
                }

                return Json(new { success = false, message = "Invalid student data. Please check your input." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Student List
        public ActionResult StudentList()
        {
            // Fetch all students from the database
            var students = _context.Students.ToList();
            return View(students); // Pass the students list to the view
        }

        // Dispose of the context to release resources when done
        protected override void Dispose(bool disposing)
        {
            //_context.Dispose(); // Release the context resources
            base.Dispose(disposing);
        }


        public ActionResult Edit(int id)
        {
            // Fetch the student by ID from the database
            var student = _context.Students.Find(id); 
            if (student == null)
            {
                return HttpNotFound(); // Return 404 if student is not found
            }
            return View(student); // Pass the student object to the view
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            // Exclude Password from validation
            ModelState.Remove("Password");
            string userSlug=SlugHelper.CreateSlug(student.Name);   
            
            if (ModelState.IsValid)
            {
                // Fetch and update the student record
                var existingStudent = _context.Students.SingleOrDefault(s => s.Id == student.Id);
                if (existingStudent != null)
                {
                    existingStudent.Name = student.Name;
                    existingStudent.Username = userSlug;
                    existingStudent.Email = student.Email;
                    existingStudent.DateOfBirth = student.DateOfBirth;

                    _context.SaveChanges(); // Save changes to the database
                    return RedirectToAction("StudentList"); // Redirect back to the student list
                }
                return HttpNotFound(); // Return 404 if the student is not found
            }

            // Log validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
            }

            // Return the view with validation errors
            return View("Edit", student);
        }

        // GET: Student/Delete/{id}
        public ActionResult Delete(int id)
        {
            // Fetch the student by ID
            var student = _context.Students.SingleOrDefault(s => s.Id == id);

            // If the student doesn't exist, return NotFound
            if (student == null)
            {
                return HttpNotFound();
            }

            // Pass the student to the view for confirmation
            return View(student);
        }


        // POST: Student/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Fetch the student by ID
            var student = _context.Students.SingleOrDefault(s => s.Id == id);

            // If the student doesn't exist, return NotFound
            if (student == null)
            {
                return HttpNotFound();
            }

            // Remove the student from the database
            _context.Students.Remove(student);

            // Save changes to the database
            _context.SaveChanges();

            // Redirect to the StudentList page after deletion
            return RedirectToAction("StudentList");
        }




    }
}
