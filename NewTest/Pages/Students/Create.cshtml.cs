using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewTest.Data;
using NewTest.Modles;

namespace NewTest.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly NewTest.Data.SchoolContext _context;

        public CreateModel(NewTest.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            var emptyStudent=new Student();

            if(await TryUpdateModelAsync<Student>(
                emptyStudent,
                "student",
                s=>s.FirstMidName,s=>s.LastName,s => s.EnrollmentDate))
            {
                _context.Students.Add(emptyStudent);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            return Page();

        }
    }
}
