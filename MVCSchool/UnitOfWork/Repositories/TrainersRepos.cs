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
        public ApplicationDbContext ApplicationDbContext { get { return Context as ApplicationDbContext; }
            set { }
        }
        public TrainersRepos(ApplicationDbContext context) : base(context)
        {
          
        }

        public void AssignCoursesToTrainer(Trainer trainer, int[] courseEdit)
        {
            if (!(courseEdit is null))
            {
                foreach (var id in courseEdit)
                {
                    var course = ApplicationDbContext.CoursesDbSet.Find(id);
                    if (!(course is null ))
                    {
                        trainer.Courses.Add(course);
                    }
                }
                ApplicationDbContext.SaveChanges();
            }
        }

        public void ClearTrainersCourses(Trainer trainer)
        {
            trainer.Courses.Clear();
            ApplicationDbContext.SaveChanges();
        }

        public void FindTrainersCourses(Trainer trainer)
        {
            ApplicationDbContext.TrainersDbSet.Attach(trainer);

            ApplicationDbContext.Entry(trainer).Collection("Courses").Load();
        }
    }
}