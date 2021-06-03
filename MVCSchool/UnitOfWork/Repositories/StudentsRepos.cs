using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MVCSchool.DataAccessLayer;
using MVCSchool.Models;

namespace MVCSchool.UnitOfWork.Repositories
{
    public class StudentsRepos : Repository<Student>
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public StudentsRepos(ApplicationDbContext context) : base(context)
        {
          
        }
    }
}