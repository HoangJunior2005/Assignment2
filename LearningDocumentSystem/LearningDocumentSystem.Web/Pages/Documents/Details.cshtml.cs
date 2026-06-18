using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningDocumentSystem.Web.Pages.Documents
{
    public class DetailsModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            return RedirectToPage("./Detail", new { id = id });
        }
    }
}
