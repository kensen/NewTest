using System.ComponentModel.DataAnnotations;

namespace NewTest.Modles.SchoolViewModels
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }
        public int StudentCount { get;set; }
    }
}
