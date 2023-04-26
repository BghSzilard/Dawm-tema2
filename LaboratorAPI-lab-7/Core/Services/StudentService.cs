using Core.Dtos;
using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Enums;
using DataLayer.Mapping;

namespace Core.Services
{
    public class StudentService
    {
        private readonly UnitOfWork unitOfWork;
        private AuthorizationService authService { get; set; }
        public StudentService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            //this.authService = authService;
        }
        public string GetRole(User user)
        {
            return user.RoleType.ToString();
        }
        public string Validate(LoginDto payload)
        {
            var student = unitOfWork.Students.GetByEmail(payload.Email);
            var passwordFine = authService.VerifyHashedPassword(student.PasswordHash, payload.Password);

            if (passwordFine)
            {
                return authService.GetToken(student);
            }
            else
            {
                return null;
            }
        }
        public List<Student> GetAll()
        {
            var results = unitOfWork.Students.GetAll();
            return results;
        }
        public GradesByStudent GetGradesById(int studentId)
        {
            var studentWithGrades = unitOfWork.Students.GetByIdWithGrades(studentId);
            var result = new GradesByStudent(studentWithGrades);
            return result;
        }

        public void Register(StudentRegisterDto studentRegisterDto)
        {
            if (studentRegisterDto == null)
            {
                return;
            }
            var hashedPassword = authService.HashPassword(studentRegisterDto.Password);
            var student = new Student
            {
                FirstName = studentRegisterDto.FirstName,
                LastName = studentRegisterDto.LastName,
                Email = studentRegisterDto.Email,
                PasswordHash = hashedPassword,
                RoleType = Role.RoleType.Student
            };
            unitOfWork.Students.Insert(student);
            unitOfWork.SaveChanges();
        }
        public Dictionary<int, List<Student>> GetGroupedStudents()
        {
            var results = unitOfWork.Students.GetGroupedStudents();

            return results;
        }
    }
}
