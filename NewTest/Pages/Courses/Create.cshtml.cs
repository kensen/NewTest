using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewTest.Data;
using NewTest.Modles;

namespace NewTest.Pages.Courses
{
    public class CreateModel : DepartmentNamePageModel
    {
        private readonly NewTest.Data.SchoolContext _context;

        public CreateModel(NewTest.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId");
            PopulateDepartmentsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Courses.Add(Course);
            //await _context.SaveChangesAsync();

            //return RedirectToPage("./Index");

            var emptCourse = new Course();
            if(await TryUpdateModelAsync<Course>(
                emptCourse,
                "course",
                s=>s.CourseID,s=>s.DepartmentID,s=>s.Title,s=>s.Credits                
                )) 
            { 
                _context.Courses.Add(emptCourse);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateDepartmentsDropDownList(_context, emptCourse.DepartmentID);
            return Page();
        }
    }
}
