using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningDocumentSystem.Entities.Models
{
    public class ChatMessage
    {
        [Key]
        [Column("MessageID")]
        public int MessageID { get; set; }

        [Required]
        public int SessionID { get; set; }

        /// <summary>"user" hoặc "assistant"</summary>
        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "user";

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; } = string.Empty;

        /// <summary>JSON serialized list of ChatSourceDto, nullable</summary>
        [Column(TypeName = "nvarchar(max)")]
        public string? SourcesJson { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(SessionID))]
        public virtual ChatSession Session { get; set; } = null!;
    }
}
