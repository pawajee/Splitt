using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Respository.Repository
{
    public class DocumentLibraryRepository : Repository<DocumentLibrary>, IDocumentLibraryRepository
    {
        protected readonly SplittAppContext _context;

        public DocumentLibraryRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }
        //
        public async Task<DocumentLibrary?> GetDocumentLibrary(Guid docId)
        {
            var obj = _context.DocumentLibrary.Include(t => t.DocumentCategory).
                Where(t => t.Id == docId);
            return await obj.FirstOrDefaultAsync();

        }

    }
}
