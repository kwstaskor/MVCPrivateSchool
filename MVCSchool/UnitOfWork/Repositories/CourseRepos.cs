using MVCSchool.DataAccessLayer;
using MVCSchool.Models;

namespace MVCSchool.UnitOfWork.Repositories
{
    public class CourseRepos : Repository<Course>
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public CourseRepos(ApplicationDbContext context)
            : base(context)
        {

        }
    }
}