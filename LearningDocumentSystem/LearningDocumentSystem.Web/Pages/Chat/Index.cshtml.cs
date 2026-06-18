using LearningDocumentSystem.Business.DTOs;
using LearningDocumentSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Pages.Chat
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IChatService _chatService;
        private readonly ISubjectService _subjectService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            IChatService chatService,
            ISubjectService subjectService,
            ILogger<IndexModel> logger)
        {
            _chatService = chatService;
            _subjectService = subjectService;
            _logger = logger;
        }

        public IEnumerable<SubjectDto> Subjects { get; set; } = [];

        public async Task OnGetAsync()
        {
            Subjects = await _subjectService.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAskAsync(string question, int? subjectId, int? chapterId)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                return new JsonResult(new { answer = "Vui lòng nhập câu hỏi hợp lệ." });
            }

            try
            {
                var result = await _chatService.AskQuestionAsync(question.Trim(), subjectId, chapterId);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing chat question in Razor Page.");
                return new JsonResult(new { answer = "Đã xảy ra lỗi hệ thống khi xử lý câu hỏi của bạn. Vui lòng thử lại sau." });
            }
        }
    }
}
