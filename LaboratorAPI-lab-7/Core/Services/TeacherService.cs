using Core.Dtos;
using DataLayer.Entities;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TeacherService
    {
        private readonly UnitOfWork unitOfWork;
        private AuthorizationService authService { get; set; }
        public TeacherService(UnitOfWork unitOfWork, AuthorizationService authorizationService)
        {
            this.unitOfWork = unitOfWork;
            this.authService = authorizationService;
        }
        public void Register(TeacherRegisterDto teacherRegisterData)
        {
            if (teacherRegisterData == null)
            {
                return;
            }
            var hashedPassword = authService.HashPassword(teacherRegisterData.Password);
            var teacher = new Teacher
            {
                FirstName = teacherRegisterData.FirstName,
                LastName = teacherRegisterData.LastName,
                Email = teacherRegisterData.Email,
                PasswordHash = hashedPassword,
                RoleType = Role.RoleType.Teacher
            };
            unitOfWork.Teachers.Insert(teacher);
            unitOfWork.SaveChanges();
        }
        public string GetRole(User user)
        {
            return user.RoleType.ToString();
        }
        public string Validate(LoginDto payload)
        {
            var teacher = unitOfWork.Teachers.GetByEmail(payload.Email);
            var passwordFine = authService.VerifyHashedPassword(teacher.PasswordHash, payload.Password);
            if (passwordFine)
            {
                return authService.GetToken(teacher);
            }
            else
            {
                return null;
            }
        }
        public List<Teacher> GetAll()
        {
            var result = unitOfWork.Teachers.GetAll();
            return result;
        }
    }
}
