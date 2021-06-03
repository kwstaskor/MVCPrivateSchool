using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MVCSchool.DataAccessLayer;
using MVCSchool.Models;

namespace MVCSchool.UnitOfWork.Repositories
{
    public class TrainersRepos : Repository<Trainer>
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public TrainersRepos(ApplicationDbContext context) : base(context)
        {
        }
    }
}