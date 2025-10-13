namespace ProjectLab.Models
{
    public class InstructorCourse
    {
        public int InstructorId { get; set; }
        public int CourseId { get; set; }
        public int RateHour { get; set; }
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}
