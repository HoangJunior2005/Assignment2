using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Documents
{
    [Authorize]
    public class DetailModel : PageModel
    {
        private readonly IDocumentService _documentService;

        public DetailModel(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public DocumentDetailDto Document { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var doc = await _documentService.GetDetailAsync(id);
            if (doc == null) return NotFound();

            Document = doc;
            return Page();
        }
    }
}
