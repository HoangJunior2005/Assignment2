using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Chapters
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IChapterService _chapterService;
        private readonly ISubjectService _subjectService;

        public IndexModel(IChapterService chapterService, ISubjectService subjectService)
        {
            _chapterService = chapterService;
            _subjectService = subjectService;
        }

        public IEnumerable<ChapterDto> Chapters { get; set; } = [];
        public IEnumerable<SubjectDto> Subjects { get; set; } = [];

        [BindProperty(SupportsGet = true)]
        public int? SubjectId { get; set; }

        public async Task OnGetAsync()
        {
            Subjects = await _subjectService.GetAllAsync();
            Chapters = SubjectId.HasValue
                ? await _chapterService.GetBySubjectAsync(SubjectId.Value)
                : await _chapterService.GetAllAsync();
        }
    }
}
