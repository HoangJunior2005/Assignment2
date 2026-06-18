using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Chapters
{
    [Authorize(Policy = "TeacherUp")]
    public class DeleteModel : PageModel
    {
        private readonly IChapterService _chapterService;

        public DeleteModel(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var c = await _chapterService.GetByIdAsync(id);
            int? subjectId = c?.SubjectID;
            
            try
            {
                await _chapterService.DeleteAsync(id);
                TempData["Success"] = "Xóa chương học thành công.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            
            return RedirectToPage("./Index", new { subjectId = subjectId });
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("./Index");
        }
    }
}
