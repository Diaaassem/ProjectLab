using Microsoft.AspNetCore.Mvc;
using ProjectLab.Models.Validators;

namespace ProjectLab.Models
{
    [MinDegreeLessThanDegree]
    public class Course
    {
        public int Id { get; set; }
        [Remote(action: "IsCourseNameUnique", controller: "Course", AdditionalFields = "Id", ErrorMessage = "Course name must be unique.")]
        public string Name { get; set; }
        public string Topic { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public ICollection<StudentCourse> Registrations { get; set; } = new List<StudentCourse>();
        public ICollection<InstructorCourse> TeachCourses { get; set; } = new List<InstructorCourse>();
    }
}
