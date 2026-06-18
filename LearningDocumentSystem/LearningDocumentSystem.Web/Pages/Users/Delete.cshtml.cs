using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Users
{
    [Authorize(Policy = "AdminOnly")]
    public class DeleteModel : PageModel
    {
        private readonly IAdminUserService _adminUserService;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(IAdminUserService adminUserService, ILogger<DeleteModel> logger)
        {
            _adminUserService = adminUserService;
            _logger = logger;
        }

        [BindProperty]
        public int UserID { get; set; }

        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var users = await _adminUserService.GetAllUsersAsync();
            var user = users.FirstOrDefault(u => u.UserID == id);
            if (user == null) return NotFound();

            UserID = user.UserID;
            Username = user.Username;
            FullName = user.FullName;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _adminUserService.DeleteUserAsync(UserID);
                TempData["Success"] = "Xóa người dùng thành công.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user {UserId}.", UserID);
                TempData["Error"] = ex.Message;
                return RedirectToPage("./Index");
            }
        }
    }
}
