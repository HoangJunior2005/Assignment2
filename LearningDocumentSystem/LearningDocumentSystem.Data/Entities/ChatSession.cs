using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningDocumentSystem.Entities.Models
{
    public class ChatSession
    {
        [Key]
        [Column("SessionID")]
        public int SessionID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = "Phiên hội thoại mới";

        public int? SubjectId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(UserID))]
        public virtual User User { get; set; } = null!;

        public virtual ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}
