using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using LearningDocumentSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Subjects
{
    [Authorize(Policy = "TeacherUp")]
    public class EditModel : PageModel
    {
        private readonly ISubjectService _subjectService;

        public EditModel(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [BindProperty]
        public SubjectFormViewModel Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var s = await _subjectService.GetByIdAsync(id);
            if (s == null) return NotFound();

            Input = new SubjectFormViewModel
            {
                SubjectID = s.SubjectID,
                SubjectName = s.SubjectName,
                SubjectCode = s.SubjectCode
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                await _subjectService.UpdateAsync(new UpdateSubjectDto
                {
                    SubjectID = Input.SubjectID,
                    SubjectName = Input.SubjectName,
                    SubjectCode = Input.SubjectCode
                });
                TempData["Success"] = "Cập nhật môn học thành công.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
