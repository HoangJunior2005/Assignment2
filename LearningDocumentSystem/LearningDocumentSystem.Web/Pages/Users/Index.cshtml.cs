using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using LearningDocumentSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Users
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

        public UserRoleManageViewModel ModelData { get; set; } = new();

        public async Task OnGetAsync()
        {
            var users = await _adminUserService.GetAllUsersAsync();
            var roles = await _adminUserService.GetAllRolesAsync();

            ModelData = new UserRoleManageViewModel
            {
                Roles = roles,
                Users = users.Select(u => new UserRoleItemViewModel
                {
                    UserID = u.UserID,
                    Username = u.Username,
                    FullName = u.FullName,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    CanUpload = u.CanUpload,
                    Roles = u.Roles,
                    AssignedRoleIds = roles
                        .Where(r => u.Roles.Contains(r.RoleName))
                        .Select(r => r.RoleID)
                        .ToList()
                })
            };
        }

        // AJAX POST handler to update upload permission
        public async Task<IActionResult> OnPostUpdateUploadPermissionAsync(int userId, bool canUpload)
        {
            try
            {
                await _adminUserService.UpdateUploadPermissionAsync(userId, canUpload);
                return new JsonResult(new { success = true, message = "Cập nhật quyền upload thành công." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating upload permission for user {UserId}.", userId);
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }
    }
}
