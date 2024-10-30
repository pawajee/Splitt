namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ICountryRepository Countries { get; }
        public IDocumentLibraryRepository DocumentLibrarys { get; }
        public IDocumentConfigurationRepository DocumentConfigurations { get; }
        public IDocumentCategoryRepository DocumentCategories { get; }
        public IGenderRepository Genders { get; }
        public ILanguageRepository Languages { get; }
        public ILocationRepository Locations { get; }
        public IMerchantAverageOrderRepository MerchantAverageOrders { get; }
        public IMerchantBusinessTypeRepository MerchantBusinessTypes { get; }
        public IMerchantCategoryRepository MerchantCategories { get; }
        public IMerchantRequestAttachmentRepository MerchantRequestAttachments { get; }
        public IMerchantRequestHistoryRepository MerchantRequestHistory { get; }
        public IMerchantRequestRepository MerchantRequest { get; }
        public INationalityRepository Nationalities { get; }
        public IRequestStatusRepository RequestStatuses { get; }
        public IUsersRepository Users { get; }
        public IUserTypeRepository UserTypes { get; }

        public IMerchantAnnualSaleRepository MerchantAnnualSales{ get; }
    public Task<int> CompleteAsync();

    }
}
