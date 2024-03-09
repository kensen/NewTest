using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewTest.Data;
using NewTest.Modles;

namespace NewTest.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly NewTest.Data.SchoolContext _context;
        private readonly IConfiguration configuration;

        public IndexModel(NewTest.Data.SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Student> Students { get;set; } = default!;
        //public IList<Student> Students { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder,string currentFilter,
            string searchString,int? pageIndex) //
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            //使用IQueryable 对象使用数据库进行Where 语句执行，如果使用  IEnumerable 集合 则先从数据库全量取出之后内存进行筛选，性能较低
            IQueryable<Student> studentsIQ = from s in _context.Students
                select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                studentsIQ = studentsIQ.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                                   || s.FirstMidName.ToUpper().Contains(searchString.ToUpper()));

            }

            studentsIQ = sortOrder switch
            {
                "name_desc" => studentsIQ.OrderByDescending(s => s.LastName),
                "Date" => studentsIQ.OrderBy(s => s.EnrollmentDate),
                "date_desc" => studentsIQ.OrderByDescending(s => s.EnrollmentDate),
                _ => studentsIQ.OrderBy(s => s.LastName)
            };

            //Students = await _context.Students.Take(10).ToListAsync();
            //Students=await studentsIQ.AsNoTracking().ToListAsync();

            var pageSize = configuration.GetValue("PageSize", 4);
            Students = await PaginatedList<Student>.CreateAsync(studentsIQ.AsNoTracking(),
                pageIndex ?? 1, pageSize);
        }
    }
}
