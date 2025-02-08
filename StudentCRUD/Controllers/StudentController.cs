using Microsoft.AspNetCore.Mvc;
using StudentCRUD.Models;

namespace StudentCRUD.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _context;

        public StudentController(StudentContext context)
        {
            _context = context;
        }

        // ✅ Fetch students as JSON for AJAX
        public JsonResult GetStudents()
        {
            var students = _context.Student.ToList();
            return Json(students);
        }

        // ✅ Create a new student (AJAX)
        [HttpPost]
        public JsonResult CreateStudent([FromBody] Student std)
        {
            if (std != null)
            {
                _context.Student.Add(std);
                _context.SaveChanges();
                var createdStudent = _context.Student.Find(std.StudentID);
                return Json(new { success = true, message = "Student added successfully!", std = createdStudent });
            }
            return Json(new { success = false, message = "Error adding student" });
        }
        public IActionResult AddStudent() // Or Create() if you prefer
        {
            return View("CreateStudent"); // Returns the CreateStudent view
        }

        // ✅ Show student details (AJAX)
        public JsonResult ShowStudent(int id)
        {
            var selectedStudent = _context.Student.Find(id);
            return Json(selectedStudent);
        }

        // ✅ Edit student details
        // GET: Edit Student
        public IActionResult EditStudent(int id)
        {
            var student = _context.Student.Find(id);
            return View(student); // Pass the student to the view
        }

        // POST: Edit Student
        [HttpPost]
        public IActionResult EditStudent(int id, Student student) // No model binding here for simplicity
        {
            // Update the student's properties (you might want to check for nulls, etc.)
            var existingStudent = _context.Student.Find(id);
            if (existingStudent != null)
            {
                existingStudent.StudentName = student.StudentName;
                existingStudent.StudentAddress = student.StudentAddress;

                _context.Update(existingStudent);
                _context.SaveChanges();
            }


            return RedirectToAction("Index"); // Redirect back to the list
        }

        // Delete A Student

        // GET: Delete Confirmation (Displays the confirmation view)
        public IActionResult DeleteConfirmation(int id)
        {
            var student = _context.Student.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [Route("Student/DeleteStudent/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var studentToDelete = _context.Student.Find(id);
            if (studentToDelete != null)
            {
                _context.Student.Remove(studentToDelete);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
