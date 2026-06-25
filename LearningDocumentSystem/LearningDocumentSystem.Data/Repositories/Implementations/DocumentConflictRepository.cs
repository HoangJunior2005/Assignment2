using LearningDocumentSystem.Data.DbContexts;
using LearningDocumentSystem.Data.Repositories.Interfaces;
using LearningDocumentSystem.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Data.Repositories.Implementations
{
    public class DocumentConflictRepository : GenericRepository<DocumentConflict>, IDocumentConflictRepository
    {
        public DocumentConflictRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<DocumentConflict>> GetByDocumentIdAsync(int documentId)
        {
            return await _context.DocumentConflicts
                .Include(dc => dc.ConflictingDocument)
                .Include(dc => dc.Chunk)
                .Include(dc => dc.ConflictingChunk)
                .Where(dc => dc.DocumentID == documentId)
                .ToListAsync();
        }
    }
}
