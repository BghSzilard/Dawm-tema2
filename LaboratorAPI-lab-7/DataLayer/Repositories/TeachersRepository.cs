using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class TeachersRepository : RepositoryBase<Teacher>
    {
        private readonly AppDbContext dbContext;

        public TeachersRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public Teacher GetByEmail(string email)
        {
            return dbContext.Teachers.FirstOrDefault(t => t.Email == email);
        }
    }
}
