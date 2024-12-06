using MachineTestABM.Data;
using MachineTestABM.DTO;
using MachineTestABM.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MachineTestABM.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public StudentController(ApplicationDbContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(StudentCreateDTO studentCreateDTO)
        {
            if (studentCreateDTO.ImageUp != null)
            {
                FileInfo fileInfo = new FileInfo(studentCreateDTO.ImageUp.FileName);
                string extn = fileInfo.Extension.ToLower();
                Guid id = Guid.NewGuid();
                string filename = id.ToString() + extn;

                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/students");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + filename;
                string imagePath = Path.Combine(uploadsFolder, uniqueFileName);
                studentCreateDTO.ImageUp.CopyTo(new FileStream(imagePath, FileMode.Create));
                studentCreateDTO.Image = uniqueFileName;
            }

            Student student = new Student();

            student.Name = studentCreateDTO.Name;
            student.Address = studentCreateDTO.Address;
            student.Phone1 = studentCreateDTO.Phone1;
            student.Phone2 = studentCreateDTO.Phone2;
            student.Email = studentCreateDTO.Email;
            student.DOB = studentCreateDTO.DOB;
            student.Image = studentCreateDTO.Image;
            student.City = studentCreateDTO.City;
            student.State = studentCreateDTO.State;
           
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();

            return RedirectToAction("StudentView" ,"Student");

           // return View();
        }

        [HttpGet]
        public IActionResult StudentView()
        {
            var students = _dbContext.Students.ToList();
            return View(students);
        }

        //[HttpGet]
        //public IActionResult EditStudent(int StudentId)
        //{
        //    var student = _dbContext.Students.Find(StudentId);
        //    return View(student);
        //}

        [HttpGet]
        public IActionResult EditStudent(int studentId)
        {
            var student = _dbContext.Students.Find(studentId);
          
            var studentEditDTO = new StudentEditDTO
            {
                StudentId = student.StudentId,
                Name = student.Name,
                Address = student.Address,
                Phone1 = student.Phone1,
                Phone2 = student.Phone2,
                Email = student.Email,
                DOB = student.DOB,
                City = student.City,
                State = student.State,
                Image = student.Image 
            };

            return View(studentEditDTO);
        }


        [HttpPost]
        public IActionResult EditStudent(StudentEditDTO studentEditDTO)
        {
            var student = _dbContext.Students.Find(studentEditDTO.StudentId);

            if(student != null)
            {
                if (studentEditDTO.ImageUp != null)
                {
                    FileInfo fileInfo = new FileInfo(studentEditDTO.ImageUp.FileName);
                    string extn = fileInfo.Extension.ToLower();
                    Guid id = Guid.NewGuid();
                    string filename = id.ToString() + extn;

                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/students");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + filename;
                    string imagePath = Path.Combine(uploadsFolder, uniqueFileName);
                    studentEditDTO.ImageUp.CopyTo(new FileStream(imagePath, FileMode.Create));
                    studentEditDTO.Image = uniqueFileName;
                }

                student.Name= studentEditDTO.Name;
                student.Address= studentEditDTO.Address;
                student.Phone1= studentEditDTO.Phone1;
                student.Phone2= studentEditDTO.Phone2;
                student.Email= studentEditDTO.Email;
                student.DOB= studentEditDTO.DOB;
                student.Image= studentEditDTO.Image;
                student.City= studentEditDTO.City;
                student.State = studentEditDTO.State;

                _dbContext.SaveChanges();
            }

            // return View();

            return RedirectToAction("StudentView", "Student");
        }


        [HttpPost]
        public IActionResult DeleteStudent(int StudentId)
        {
            var student = _dbContext.Students.FirstOrDefault(x=>x.StudentId == StudentId);
            if(student != null)
            {
                _dbContext.Students.Remove(student);
                _dbContext.SaveChanges();
            }
            //return View();

            return RedirectToAction("StudentView", "Student");
        }


    }
}
