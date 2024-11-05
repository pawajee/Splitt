using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Respository.Repository;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Repository
{//
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly SplittAppContext _context;

        //::Repository Definitions::
        public IEmailNotificationRepository EmailNotifications { get; private set; }
        public ICustomerRegistrationRequestRepository CustomerRegistrationRequests { get; private set; }
        public ISmsNotificationRepository SmsNotifications { get; private set; }
        public IPrePaymentRepository PrePayments { get; private set; }
        public IPaymentInstallmentRepository PaymentInstallments { get; private set; }
        public IPaymentRepository Payments { get; private set; }
        public IOrderItemRepository OrderItems { get; private set; }
        public IMidRequestLogRepository MidRequestLogs { get; private set; }
        public ILkPaymentStatusRepository LkPaymentStatuses { get; private set; }
        public ILkPaymentRequestTypeRepository LkPaymentRequestTypes { get; private set; }
        public ILkPaymentOptionRepository LkPaymentOptions { get; private set; }
        public ILkPaymentBrandTypeRepository LkPaymentBrandTypes { get; private set; }
        public ILkOtpPurposeRepository LkOtpPurposes { get; private set; }
        public ILkOrderStatusRepository LkOrderStatuses { get; private set; }
        public ILkMidRequestTypeRepository LkMidRequestTypes { get; private set; }
        public ILkMidRequestStatusRepository LkMidRequestStatuses { get; private set; }
        public ILkMartialStatusRepository LkMartialStatuses { get; private set; }
        public ILkInstallmentTypeRepository LkInstallmentTypes { get; private set; }
        public ILkEmploymentStatusRepository LkEmploymentStatuses { get; private set; }
        public ILkEmploymentSectorRepository LkEmploymentSectors { get; private set; }
        public ILkEducationalLevelRepository LkEducationalLevels { get; private set; }

        public ILkCustomerStatusRepository LkCustomerStatuses { get; private set; }
        public ILkCurrencyRepository LkCurrencies { get; private set; }
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
        public IOrderRepository Orders { get; private set; }
        public UnitOfWork(SplittAppContext context)
        {
            _context = context;
            //::Declare Repository::
            EmailNotifications = new EmailNotificationRepository(_context);
            CustomerRegistrationRequests = new CustomerRegistrationRequestRepository(_context);
            SmsNotifications = new SmsNotificationRepository(_context);
            PrePayments = new PrePaymentRepository(_context);
            PaymentInstallments = new PaymentInstallmentRepository(_context);
            Payments = new PaymentRepository(_context);
            OrderItems = new OrderItemRepository(_context);
            MidRequestLogs = new MidRequestLogRepository(_context);
            LkPaymentStatuses = new LkPaymentStatusRepository(_context);
            LkPaymentRequestTypes = new LkPaymentRequestTypeRepository(_context);
            LkPaymentOptions = new LkPaymentOptionRepository(_context);
            LkPaymentBrandTypes = new LkPaymentBrandTypeRepository(_context);
            LkOtpPurposes = new LkOtpPurposeRepository(_context);
            LkOrderStatuses = new LkOrderStatusRepository(_context);
            LkMidRequestTypes = new LkMidRequestTypeRepository(_context);
            LkMidRequestStatuses = new LkMidRequestStatusRepository(_context);
            LkMartialStatuses = new LkMartialStatusRepository(_context);
            LkInstallmentTypes = new LkInstallmentTypeRepository(_context);
            LkEmploymentStatuses = new LkEmploymentStatusRepository(_context);
            LkEmploymentSectors = new LkEmploymentSectorRepository(_context);
            LkEducationalLevels = new LkEducationalLevelRepository(_context);
            LkCustomerStatuses = new LkCustomerStatusRepository(_context);
            LkCurrencies = new LkCurrencyRepository(_context);
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
            Orders = new OrderRepository(_context);

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
