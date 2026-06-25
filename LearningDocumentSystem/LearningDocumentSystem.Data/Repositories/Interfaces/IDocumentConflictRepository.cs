using LearningDocumentSystem.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Data.Repositories.Interfaces
{
    public interface IDocumentConflictRepository : IGenericRepository<DocumentConflict>
    {
        Task<IEnumerable<DocumentConflict>> GetByDocumentIdAsync(int documentId);
    }
}
