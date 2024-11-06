using Duc.Splitt.Data.DataAccess.Models;

namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IDocumentLibraryRepository : IRepository<DocumentLibrary>
    {
        Task<DocumentLibrary?> GetDocumentLibrary(Guid docId);
    }
}
