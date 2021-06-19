using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using MVCSchool.DataAccessLayer;
using MVCSchool.Models;
using MVCSchool.Models.ViewModels;

namespace MVCSchool.UnitOfWork.Repositories
{
    public class TrainersRepos : Repository<Trainer>
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public TrainersRepos(ApplicationDbContext context) : base(context)
        {
        }

        public void AssignCoursesToTrainer(Trainer trainer, int[] courseEdit)
        {
            if (courseEdit is null) return;

            foreach (var id in courseEdit)
            {
                var course = DbContext.CoursesDbSet.Find(id);
                if (!(course is null ))
                {
                    trainer.Courses.Add(course);
                }
            }
            DbContext.SaveChanges();
        }

        public void ClearTrainerCourses(Trainer trainer)
        {
            trainer.Courses.Clear();
            DbContext.SaveChanges();
        }

        public void AttachTrainerCourses(Trainer trainer)
        {
            DbContext.TrainersDbSet.Attach(trainer);

            DbContext.Entry(trainer).Collection("Courses").Load();
        }
    }
}