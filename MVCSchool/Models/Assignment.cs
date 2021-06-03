using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using MVCSchool.Models.Validations;

namespace MVCSchool.Models
{
    [Validator(typeof(AssignmentValidate))]
    public class Assignment
    {
        public Assignment()
        {
            Courses = new HashSet<Course>();
            Students = new HashSet<Student>();
        }
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name = "Submission Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:MM }", ApplyFormatInEditMode = true)]
        public DateTime Submission { get; set; }

        [Display(Name = "Oral Mark")]
        public float OralMark { get; set; }

        [Display(Name = "Total Mark")]
        public float TotalMark { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}