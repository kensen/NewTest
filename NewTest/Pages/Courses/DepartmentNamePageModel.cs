using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewTest.Data;
using NewTest.Modles;
using System.Linq;

namespace NewTest.Pages.Courses
{
    public class DepartmentNamePageModel:PageModel
    {
        public SelectList DepartmentNameSL { get; set; }

        public void PopulateDepartmentsDropDownList(SchoolContext _context,
            object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Departments
                                   orderby d.Name // Sort by name.
                                   select d;

            DepartmentNameSL = new SelectList(departmentsQuery.AsNoTracking(),
                nameof(Department.DepartmentId),
                nameof(Department.Name),
                selectedDepartment);
        }
    }
}
