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
    public class CreateModel : PageModel
    {
        private readonly ISubjectService _subjectService;

        public CreateModel(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [BindProperty]
        public SubjectFormViewModel Input { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                await _subjectService.CreateAsync(new CreateSubjectDto
                {
                    SubjectName = Input.SubjectName,
                    SubjectCode = Input.SubjectCode
                });
                TempData["Success"] = "Tạo môn học thành công.";
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
