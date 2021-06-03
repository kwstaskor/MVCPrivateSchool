using System;
using MVCSchool.UnitOfWork.Repositories;

namespace MVCSchool.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        CourseRepos Courses { get; }
        AssignmentsRepos Assignments { get; }
        TrainersRepos Trainers { get; }
        StudentsRepos Students { get; }
        void Save();
    }
}