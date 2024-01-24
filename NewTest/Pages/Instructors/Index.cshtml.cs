using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewTest.Data;
using NewTest.Modles;
using NewTest.Modles.SchoolViewModels;

namespace NewTest.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly NewTest.Data.SchoolContext _context;

        public IndexModel(NewTest.Data.SchoolContext context)
        {
            _context = context;
        }

       // public IList<Instructor> Instructor { get;set; } = default!;
        public InstructorIndexData InstructorData { get; set; }
        public int InstructorID { get; set; }
        public int CourseID {  get; set; }  


        public async Task OnGetAsync(int? id,int? courseID)
        {
           // Instructor = await _context.Instructors.ToListAsync();

            InstructorData=new InstructorIndexData();
            InstructorData.Instructors=await _context.Instructors
                .Include(i=>i.OfficeAssignment)
                .Include(i=>i.Courses)
                .ThenInclude(c=>c.Department)
                .OrderBy(i=>i.LastName)
                .ToListAsync();

            if(id!=null)
            {
                InstructorID = id.Value;
                Instructor instructor=InstructorData.Instructors
                    .Where(i=>i.ID==id.Value).Single();

                InstructorData.Courses = instructor.Courses;
            }

            if(courseID!=null)
            {
                CourseID = courseID.Value;
                IEnumerable<Enrollment> Enrollments = await _context.Enrollments
                    .Where(x => x.CourseID == CourseID)
                    .Include(i => i.Student)
                    .ToListAsync();
                InstructorData.Enrollments = Enrollments; 
            }
        }
    }
}
