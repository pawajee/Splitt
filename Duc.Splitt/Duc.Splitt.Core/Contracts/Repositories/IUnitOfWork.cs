namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ILkCountryRepository LkCountries { get; }
        public IDocumentLibraryRepository DocumentLibrarys { get; }
        public ILkDocumentConfigurationRepository LkDocumentConfigurations { get; }
        public ILkDocumentCategoryRepository LkDocumentCategories { get; }
        public ILkGenderRepository LkGenders { get; }
        public ILkLanguageRepository LkLanguages { get; }
        public ILkLocationRepository LkLocations { get; }
        public ILkMerchantAverageOrderRepository LkMerchantAverageOrders { get; }
        public ILkMerchantBusinessTypeRepository LkMerchantBusinessTypes { get; }
        public ILkMerchantCategoryRepository LkMerchantCategories { get; }
        public ILkMerchantStatusRepository LkMerchantStatuses { get; }
        public ILkNationalityRepository LkNationalities { get; }
        public ILkMerchantAnnualSaleRepository LkMerchantAnnualSales { get; }
        public ILkRoleRepository LkRoles { get; }


        public IMerchantRepository Merchants { get; }
        public IMerchantAttachmentRepository MerchantAttachments { get; }
        public IMerchantHistoryRepository MerchantHistories { get; }

        public IUserRepository Users { get; }

        public IBackOfficeUserRepository BackOfficeUsers { get; }
        public ICustomerRepository Customers { get; }
        public IMerchantContactRepository MerchantContacts { get; }

        public IOtpRequestRepository OtpRequests { get; }

        public IOrderRepository Orders { get; }

        public Task<int> CompleteAsync();

    }
}
