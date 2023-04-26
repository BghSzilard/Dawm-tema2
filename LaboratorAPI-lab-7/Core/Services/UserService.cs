using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService
    {
        public string GetRole(User user)
        {
            return user.RoleType.ToString();
        }
    }
}
