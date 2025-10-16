namespace ProjectLab.Models.ViewModels
{
    public class StudentDetailsVM
    {
        public string StudentName { get; set; }
        public string DepartmentName { get; set; }
        public List<CourseGradeVM> Courses { get; set; } = new List<CourseGradeVM>();
    }

    public class CourseGradeVM
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseTopic { get; set; }
        public int CourseDegree { get; set; }
        public int CourseMinDegree { get; set; }
        public string Grade { get; set; }
    }
}
