using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Users
{
    [Authorize(Policy = "AdminOnly")]
    public class DetailsModel : PageModel
    {
        private readonly IAdminUserService _adminUserService;

        public DetailsModel(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        public UserDto UserData { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var users = await _adminUserService.GetAllUsersAsync();
            var user = users.FirstOrDefault(u => u.UserID == id);
            if (user == null) return NotFound();

            UserData = user;
            return Page();
        }
    }
}
