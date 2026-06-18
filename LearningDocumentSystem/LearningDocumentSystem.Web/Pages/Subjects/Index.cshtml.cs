using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Subjects
{
    [Authorize(Policy = "AdminOnly")]
    public class IndexModel : PageModel
    {
        private readonly ISubjectService _subjectService;

        public IndexModel(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public IEnumerable<SubjectDto> Subjects { get; set; } = [];

        public async Task OnGetAsync()
        {
            Subjects = await _subjectService.GetAllAsync();
        }
    }
}
