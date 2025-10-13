namespace ProjectLab.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public ICollection<StudentCourse> Registrations { get; set; } = new List<StudentCourse>();
        public ICollection<InstructorCourse> TeachCourses { get; set; } = new List<InstructorCourse>();

    }
}
