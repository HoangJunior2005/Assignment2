using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.AllowedEmails
{
    [Authorize(Policy = "AdminOnly")]
    public class IndexModel : PageModel
    {
        private readonly IAdminUserService _adminUserService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IAdminUserService adminUserService, ILogger<IndexModel> logger)
        {
            _adminUserService = adminUserService;
            _logger = logger;
        }

        public IEnumerable<AllowedEmailDto> Emails { get; set; } = [];

        public async Task OnGetAsync()
        {
            Emails = await _adminUserService.GetAllowedEmailsAsync();
        }

        public async Task<IActionResult> OnPostImportEmailsAsync(IFormFile file)
        {
            bool isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            try
            {
                var count = await _adminUserService.ImportAllowedEmailsAsync(file);
                var message = $"Nhập whitelist thành công. Đã thêm {count} email mới.";
                if (isAjax)
                {
                    return new JsonResult(new { message });
                }
                TempData["Success"] = message;
            }
            catch (ArgumentException ex)
            {
                if (isAjax) return new BadRequestObjectResult(new { error = ex.Message });
                TempData["Error"] = ex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing whitelisted emails.");
                var errorMsg = "Đã xảy ra lỗi hệ thống khi nhập email.";
                if (isAjax) return new StatusCodeResult(500);
                TempData["Error"] = errorMsg;
            }

            return RedirectToPage("./Index");
        }
    }
}
