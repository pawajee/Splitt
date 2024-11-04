using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Respository.Repository;

namespace Duc.Splitt.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly SplittAppContext _context;


        public ILkCountryRepository LkCountries { get; private set; }
        public IDocumentLibraryRepository DocumentLibrarys { get; private set; }
        public ILkDocumentConfigurationRepository LkDocumentConfigurations { get; private set; }
        public ILkDocumentCategoryRepository LkDocumentCategories { get; private set; }
        public ILkGenderRepository LkGenders { get; private set; }
        public ILkLanguageRepository LkLanguages { get; private set; }
        public ILkLocationRepository LkLocations { get; private set; }
        public ILkMerchantAverageOrderRepository LkMerchantAverageOrders { get; private set; }
        public ILkMerchantBusinessTypeRepository LkMerchantBusinessTypes { get; private set; }
        public ILkMerchantCategoryRepository LkMerchantCategories { get; private set; }
        public ILkMerchantStatusRepository LkMerchantStatuses { get; private set; }
        public ILkNationalityRepository LkNationalities { get; private set; }
        public ILkMerchantAnnualSaleRepository LkMerchantAnnualSales { get; private set; }
        public ILkRoleRepository LkRoles { get; private set; }


        public IMerchantRepository Merchants { get; private set; }
        public IMerchantAttachmentRepository MerchantAttachments { get; }
        public IMerchantHistoryRepository MerchantHistories { get; private set; }

        public IUserRepository Users { get; private set; }

        public IBackOfficeUserRepository BackOfficeUsers { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IMerchantContactRepository MerchantContacts { get; private set; }

        public IOtpRequestRepository OtpRequests { get; private set; }
        public UnitOfWork(SplittAppContext context)
        {
            _context = context;

            LkCountries = new LkCountryRepository(_context);
            DocumentLibrarys = new DocumentLibraryRepository(_context);
            LkDocumentConfigurations = new LkDocumentConfigurationRepository(_context);
            LkDocumentCategories = new LkDocumentCategoryRepository(_context);
            LkGenders = new LkGenderRepository(_context);
            LkLanguages = new LkLanguageRepository(_context);
            LkLocations = new LkLocationRepository(_context);
            LkNationalities = new LkNationalityRepository(_context);
            LkMerchantStatuses = new LkMerchantStatusRepository(_context);

            LkMerchantAverageOrders = new LkMerchantAverageOrderRepository(_context);
            LkMerchantBusinessTypes = new LkMerchantBusinessTypeRepository(_context);
            LkMerchantCategories = new LkMerchantCategoryRepository(_context);
            MerchantAttachments = new MerchantAttachmentRepository(_context);
            MerchantHistories = new MerchantHistoryRepository(_context);
            Merchants = new MerchantRepository(_context);

            Users = new UserRepository(_context);
            LkRoles = new LkRoleRepository(_context);
            LkMerchantAnnualSales = new LkMerchantAnnualSaleRepository(_context);
            BackOfficeUsers = new BackOfficeUserRepository(_context);
            OtpRequests = new OtpRequestRepository(_context);
            Customers = new CustomerRepository(_context);
            MerchantContacts = new MerchantContactRepository(_context);


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
