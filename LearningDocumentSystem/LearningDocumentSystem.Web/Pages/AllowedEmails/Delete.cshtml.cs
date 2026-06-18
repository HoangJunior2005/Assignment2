using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.AllowedEmails
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                await _adminUserService.DeleteAllowedEmailAsync(id);
                TempData["Success"] = "Xóa email khỏi danh sách whitelist thành công.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting whitelisted email {Id}.", id);
                TempData["Error"] = "Đã xảy ra lỗi khi xóa email.";
            }
            return RedirectToPage("./Index");
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("./Index");
        }
    }
}
