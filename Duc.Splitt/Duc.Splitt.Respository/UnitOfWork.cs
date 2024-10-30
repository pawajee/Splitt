using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Respository.Repository;

namespace Duc.Splitt.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly SplittAppContext _context;


        public ICountryRepository Countries { get; private set; }
        public IDocumentLibraryRepository DocumentLibrarys { get; private set; }
        public IDocumentConfigurationRepository DocumentConfigurations { get; private set; }
        public IDocumentCategoryRepository DocumentCategories { get; private set; }
        public IGenderRepository Genders { get; private set; }
        public ILanguageRepository Languages { get; private set; }
        public ILocationRepository Locations { get; private set; }
        public IMerchantAverageOrderRepository MerchantAverageOrders { get; private set; }
        public IMerchantBusinessTypeRepository MerchantBusinessTypes { get; private set; }
        public IMerchantCategoryRepository MerchantCategories { get; private set; }
        public IMerchantRequestAttachmentRepository MerchantRequestAttachments { get; }
        public IMerchantRequestHistoryRepository MerchantRequestHistory { get; private set; }
        public IMerchantRequestRepository MerchantRequest { get; private set; }
        public INationalityRepository Nationalities { get; private set; }
        public IRequestStatusRepository RequestStatuses { get; private set; }
        public IUsersRepository Users { get; private set; }
        public IUserTypeRepository UserTypes { get; private set; }
        public IMerchantAnnualSaleRepository MerchantAnnualSales { get; private set; }
        public UnitOfWork(SplittAppContext context)
        {
            _context = context;

            Countries = new CountryRepository(_context);
            DocumentLibrarys = new DocumentLibraryRepository(_context);
            DocumentConfigurations = new DocumentConfigurationRepository(_context);
            DocumentCategories = new DocumentCategoryRepository(_context);
            Genders = new GenderRepository(_context);
            Languages = new LanguageRepository(_context);
            Locations = new LocationRepository(_context);
            MerchantAverageOrders = new MerchantAverageOrderRepository(_context);
            MerchantBusinessTypes = new MerchantBusinessTypeRepository(_context);
            MerchantCategories = new MerchantCategoryRepository(_context);
            MerchantRequestAttachments = new MerchantRequestAttachmentRepository(_context);
            MerchantRequestHistory = new MerchantRequestHistoryRepository(_context);
            MerchantRequest = new MerchantRequestRepository(_context);
            Nationalities = new NationalityRepository(_context);
            RequestStatuses = new RequestStatusRepository(_context);
            Users = new UsersRepository(_context);
            UserTypes = new UserTypeRepository(_context);
            MerchantAnnualSales = new MerchantAnnualSaleRepository(_context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
