using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using LearningDocumentSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Chapters
{
    [Authorize(Policy = "TeacherUp")]
    public class EditModel : PageModel
    {
        private readonly IChapterService _chapterService;
        private readonly ISubjectService _subjectService;

        public EditModel(IChapterService chapterService, ISubjectService subjectService)
        {
            _chapterService = chapterService;
            _subjectService = subjectService;
        }

        [BindProperty]
        public ChapterFormViewModel Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var c = await _chapterService.GetByIdAsync(id);
            if (c == null) return NotFound();

            Input = new ChapterFormViewModel
            {
                ChapterID = c.ChapterID,
                SubjectID = c.SubjectID,
                ChapterNumber = c.ChapterNumber,
                ChapterName = c.ChapterName,
                Subjects = await _subjectService.GetAllAsync()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Input.Subjects = await _subjectService.GetAllAsync();
                return Page();
            }

            try
            {
                await _chapterService.UpdateAsync(new UpdateChapterDto
                {
                    ChapterID = Input.ChapterID,
                    SubjectID = Input.SubjectID,
                    ChapterNumber = Input.ChapterNumber,
                    ChapterName = Input.ChapterName
                });
                TempData["Success"] = "Cập nhật chương học thành công.";
                return RedirectToPage("./Index", new { subjectId = Input.SubjectID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                Input.Subjects = await _subjectService.GetAllAsync();
                return Page();
            }
        }
    }
}
