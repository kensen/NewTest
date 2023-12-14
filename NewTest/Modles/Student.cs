using System.ComponentModel;

namespace NewTest.Modles
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        [DisplayName("课程")]
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
