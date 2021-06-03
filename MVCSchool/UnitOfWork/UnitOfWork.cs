using MVCSchool.DataAccessLayer;
using MVCSchool.UnitOfWork.Repositories;

namespace MVCSchool.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public CourseRepos Courses { get; private set; }
        public AssignmentsRepos Assignments { get; private set; }
        public TrainersRepos Trainers { get; private set; }
        public StudentsRepos Students { get; private set; }

        public UnitOfWork()
        {
            context = new ApplicationDbContext();
            Courses = new CourseRepos(context);
            Assignments = new AssignmentsRepos(context);
            Trainers = new TrainersRepos(context);
            Students = new StudentsRepos(context);
        }
     

        public void Dispose()
        {
            context.Dispose();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}