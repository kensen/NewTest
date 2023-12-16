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
    public class DeleteModel : PageModel
    {
        private readonly NewTest.Data.SchoolContext _context;
        private readonly ILogger<DeleteModel> _logger;


        public DeleteModel(NewTest.Data.SchoolContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Student Student { get; set; } = default!;
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id,bool? saverChangesError=false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            if (saverChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try agin", id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                return NotFound();
            }

            try
            {
                // Student = student;
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError(ex,ErrorMessage);
                return RedirectToAction("./Delete",
                    new { id, saveChangesErro = true });
            }

           
        }
    }
}
