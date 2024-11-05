namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        //::Repository Definitions::
        public IEmailNotificationRepository EmailNotifications { get; }
        public ICustomerRegistrationRequestRepository CustomerRegistrationRequests { get; }
        public ISmsNotificationRepository SmsNotifications { get; }
        public IPrePaymentRepository PrePayments { get; }
        public IPaymentInstallmentRepository PaymentInstallments { get; }
        public IPaymentRepository Payments { get; }
        public IOrderItemRepository OrderItems { get; }
        public IMidRequestLogRepository MidRequestLogs { get; }
        public ILkPaymentStatusRepository LkPaymentStatuses { get; }
        public ILkPaymentRequestTypeRepository LkPaymentRequestTypes { get; }
        public ILkPaymentOptionRepository LkPaymentOptions { get; }
        public ILkPaymentBrandTypeRepository LkPaymentBrandTypes { get; }
        public ILkOtpPurposeRepository LkOtpPurposes { get; }
        public ILkOrderStatusRepository LkOrderStatuses { get; }
        public ILkMidRequestTypeRepository LkMidRequestTypes { get; }
        public ILkMidRequestStatusRepository LkMidRequestStatuses { get; }
        public ILkMartialStatusRepository LkMartialStatuses { get; }
        public ILkInstallmentTypeRepository LkInstallmentTypes { get; }
        public ILkEmploymentStatusRepository LkEmploymentStatuses { get; }
        public ILkEmploymentSectorRepository LkEmploymentSectors { get; }
      public ILkEducationalLevelRepository LkEducationalLevels { get; }
        public ILkCustomerStatusRepository LkCustomerStatuses { get; }
        public ILkCurrencyRepository LkCurrencies { get; }
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
