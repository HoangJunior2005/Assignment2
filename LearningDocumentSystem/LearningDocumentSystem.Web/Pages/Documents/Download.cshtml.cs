using LearningDocumentSystem.Business.Services.Interfaces;
using LearningDocumentSystem.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Documents
{
    [Authorize]
    public class DownloadModel : PageModel
    {
        private readonly IDocumentService _documentService;
        private readonly IWebHostEnvironment _env;

        public DownloadModel(IDocumentService documentService, IWebHostEnvironment env)
        {
            _documentService = documentService;
            _env = env;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var doc = await _documentService.GetDetailAsync(id);
            if (doc == null)
            {
                TempData["Error"] = AppMessages.MsgNotFound;
                return RedirectToPage("./Index");
            }

            var filePath = Path.Combine(_env.WebRootPath, AppConstants.UploadFolder, doc.StoragePath);
            if (!System.IO.File.Exists(filePath))
            {
                TempData["Error"] = "File vật lý không tồn tại trên hệ thống.";
                return RedirectToPage("./Detail", new { id = id });
            }

            var contentType = doc.FileType.ToLowerInvariant() switch
            {
                "pdf" => "application/pdf",
                "docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                _ => "application/octet-stream"
            };

            var downloadName = $"{doc.Title}.{doc.FileType}";
            return PhysicalFile(filePath, contentType, downloadName);
        }
    }
}
