using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewTest.Data;
using NewTest.Modles;

namespace NewTest.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly NewTest.Data.SchoolContext _context;

        public IndexModel(NewTest.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Course> Courses { get;set; } 
       // public IList<CourseViewModel> CourseVM { get; set; }


        public async Task OnGetAsync()
        {
            Courses = await _context.Courses
                .Include(c => c.Department)
                .AsNoTracking()
                .ToListAsync();

            //CourseVM=await _context.Courses.Select(p=>new CourseViewModel 
            //{ 
            //    CourseID=p.CourseID,
            //    Title=p.Title,
            //    DepartmentName=p.Department.Name
            //}).ToListAsync();

        }
    }

    //public class CourseViewModel
    //{
    //    public int CourseID { get; set; }
    //    public string Title { get; set; }
    //    public int Credits { get; set; }
    //    public string DepartmentName { get; set; } 
    //}
}
