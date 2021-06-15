using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using MVCSchool.Models.Validations;

namespace MVCSchool.Models
{
    [Validator(typeof(StudentValidate))]
    public class Student
    {
        public Student()
        {
           Courses= new HashSet<Course>();
           Assignments = new HashSet<Assignment>();
        }
        public int StudentId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

       
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Tuition Fee")]
        [DataType(DataType.Currency)]
        public decimal TuitionFee { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}