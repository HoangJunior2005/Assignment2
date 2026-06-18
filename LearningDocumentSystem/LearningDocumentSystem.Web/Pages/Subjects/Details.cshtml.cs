using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Subjects
{
    [Authorize(Policy = "AdminOnly")]
    public class DetailsModel : PageModel
    {
        private readonly ISubjectService _subjectService;

        public DetailsModel(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public SubjectDto Subject { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var s = await _subjectService.GetWithChaptersAsync(id);
            if (s == null) return NotFound();

            Subject = s;
            return Page();
        }
    }
}
