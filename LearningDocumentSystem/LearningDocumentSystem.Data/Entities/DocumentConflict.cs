using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LearningDocumentSystem.Entities.Models;

namespace LearningDocumentSystem.Entities.Models
{
    public class DocumentConflict
    {
        [Key]
        [Column("ConflictID")]
        public int ConflictID { get; set; }

        [Required]
        public int DocumentID { get; set; }

        [Required]
        public int ConflictingDocumentID { get; set; }

        [Required]
        public int ChunkID { get; set; }

        [Required]
        public int ConflictingChunkID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; } = string.Empty;

        public DateTime DetectedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(DocumentID))]
        public virtual Document Document { get; set; } = null!;

        [ForeignKey(nameof(ConflictingDocumentID))]
        public virtual Document ConflictingDocument { get; set; } = null!;

        [ForeignKey(nameof(ChunkID))]
        public virtual DocumentChunk Chunk { get; set; } = null!;

        [ForeignKey(nameof(ConflictingChunkID))]
        public virtual DocumentChunk ConflictingChunk { get; set; } = null!;
    }
}
