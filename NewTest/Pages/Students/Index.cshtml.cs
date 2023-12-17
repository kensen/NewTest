using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewTest.Data;
using NewTest.Modles;

namespace NewTest.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly NewTest.Data.SchoolContext _context;

        public IndexModel(NewTest.Data.SchoolContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Student> Students { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString) //
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentFilter = searchString;

            //使用IQueryable 对象使用数据库进行Where 语句执行，如果使用  IEnumerable 集合 则先从数据库全量取出之后内存进行筛选，性能较低
            IQueryable<Student> studentsIQ = from s in _context.Students
                select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                studentsIQ = studentsIQ.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                                   || s.FirstMidName.ToUpper().Contains(searchString.ToUpper()));

            }

            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }

            //Students = await _context.Students.Take(10).ToListAsync();
            Students=await studentsIQ.AsNoTracking().ToListAsync();
        }
    }
}
