using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MVCSchool.DataAccessLayer;
using MVCSchool.Models;

namespace MVCSchool.UnitOfWork.Repositories
{
    public class AssignmentsRepos : Repository<Assignment>
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public AssignmentsRepos(ApplicationDbContext context)
            : base(context)
        {

        }
    }
}