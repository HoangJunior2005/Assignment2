using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Chapters
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IChapterService _chapterService;

        public DetailsModel(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        public ChapterDto Chapter { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var c = await _chapterService.GetByIdAsync(id);
            if (c == null) return NotFound();

            Chapter = c;
            return Page();
        }
    }
}
