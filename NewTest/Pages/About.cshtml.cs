using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewTest.Data;
using NewTest.Modles.SchoolViewModels;

namespace NewTest.Pages
{
    public class AboutModel : PageModel
    {
        private readonly SchoolContext _context;
        
        public AboutModel(SchoolContext context)
        {
            _context = context;
        }
        
        public IList<EnrollmentDateGroup> Students { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<EnrollmentDateGroup> data =
                from student in _context.Students
                group student by student.EnrollmentDate into dataGroup
                select new EnrollmentDateGroup() 
                { 
                    EnrollmentDate=dataGroup.Key,
                    StudentCount=dataGroup.Count()
                };

            Students = await data.AsNoTracking().ToListAsync();
        }
    }
}
