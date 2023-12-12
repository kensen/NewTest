using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewTest.Pages
{
    public class NewTestPageModel : PageModel
    {
        private readonly ILogger<NewTestPageModel> _logger;

        public NewTestPageModel(ILogger<NewTestPageModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            
        }
    }
}
