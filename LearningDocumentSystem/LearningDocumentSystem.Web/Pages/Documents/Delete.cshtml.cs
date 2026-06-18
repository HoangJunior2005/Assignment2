using LearningDocumentSystem.Business.Services.Interfaces;
using LearningDocumentSystem.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Documents
{
    [Authorize(Policy = "TeacherUp")]
    public class DeleteModel : PageModel
    {
        private readonly IDocumentService _documentService;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(
            IDocumentService documentService,
            IWebHostEnvironment env,
            ILogger<DeleteModel> logger)
        {
            _documentService = documentService;
            _env = env;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                // Thiết lập upload path cho DocumentService
                if (_documentService is Business.Services.Implementations.DocumentService ds)
                {
                    ds.SetUploadPath(Path.Combine(_env.WebRootPath, AppConstants.UploadFolder));
                }

                await _documentService.DeleteAsync(id);
                TempData["Success"] = AppMessages.MsgDeleteSuccess;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete failed for doc {Id}.", id);
                TempData["Error"] = ex.Message;
            }
            return RedirectToPage("./Index");
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("./Index");
        }
    }
}
