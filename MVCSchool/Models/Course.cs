using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation.Attributes;
using MVCSchool.Models.Validations;

namespace MVCSchool.Models
{
    [Validator(typeof(CourseValidate))]
    public class Course
    {
        public Course()
        {
            Assignments = new HashSet<Assignment>();
            Students = new HashSet<Student>();
            Trainers = new HashSet<Trainer>();
        }
        public int CourseId { get; set; }

        public string Title { get; set; }

        public string Stream { get; set; }

        public string Type { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name="Starting Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ending Date")]
        [CourseDatesVal]
        public DateTime EndDate { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Trainer> Trainers { get; set; }

        [NotMapped] public string Duration => $"{(EndDate - StartDate).Days} Days";
    }
}