using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningDocumentSystem.Web.Pages.AllowedEmails
{
    public class CreateModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("./Index");
        }
    }
}
