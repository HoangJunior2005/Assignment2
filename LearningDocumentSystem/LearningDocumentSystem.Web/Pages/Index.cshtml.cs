using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IDocumentService _documentService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IDocumentService documentService, ILogger<IndexModel> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }

        public DashboardDto Dashboard { get; private set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Teacher"))
            {
                return RedirectToPage("/Documents/Index");
            }

            try
            {
                Dashboard = await _documentService.GetDashboardAsync();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard.");
                return RedirectToPage("/Error");
            }
        }
    }
}
