using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.AllowedEmails
{
    [Authorize(Policy = "AdminOnly")]
    public class DetailsModel : PageModel
    {
        private readonly IAdminUserService _adminUserService;

        public DetailsModel(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        public AllowedEmailDto EmailData { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var emails = await _adminUserService.GetAllowedEmailsAsync();
            var email = emails.FirstOrDefault(e => e.Id == id);
            if (email == null) return NotFound();

            EmailData = email;
            return Page();
        }
    }
}
