using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class User
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string LoginId { get; set; } = null!;

    public int UserTypeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public int CreatedAt { get; set; }

    public int? ModifiedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsSystemAccount { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<BackOfficeUser> BackOfficeUserCreatedByNavigation { get; set; } = new List<BackOfficeUser>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<BackOfficeUser> BackOfficeUserModifiedByNavigation { get; set; } = new List<BackOfficeUser>();

    [InverseProperty("User")]
    public virtual BackOfficeUser? BackOfficeUserUser { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("UserCreatedAtNavigation")]
    public virtual LkLocation CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("InverseCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Customer> CustomerCreatedByNavigation { get; set; } = new List<Customer>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Customer> CustomerModifiedByNavigation { get; set; } = new List<Customer>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<CustomerRegistrationRequest> CustomerRegistrationRequestCreatedByNavigation { get; set; } = new List<CustomerRegistrationRequest>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<CustomerRegistrationRequest> CustomerRegistrationRequestModifiedByNavigation { get; set; } = new List<CustomerRegistrationRequest>();

    [InverseProperty("User")]
    public virtual Customer? CustomerUser { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<EmailNotification> EmailNotificationCreatedByNavigation { get; set; } = new List<EmailNotification>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<EmailNotification> EmailNotificationModifiedByNavigation { get; set; } = new List<EmailNotification>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<User> InverseModifiedByNavigation { get; set; } = new List<User>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MerchantAttachment> MerchantAttachmentCreatedByNavigation { get; set; } = new List<MerchantAttachment>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<MerchantAttachment> MerchantAttachmentModifiedByNavigation { get; set; } = new List<MerchantAttachment>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MerchantContact> MerchantContactCreatedByNavigation { get; set; } = new List<MerchantContact>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<MerchantContact> MerchantContactModifiedByNavigation { get; set; } = new List<MerchantContact>();

    [InverseProperty("User")]
    public virtual MerchantContact? MerchantContactUser { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Merchant> MerchantCreatedByNavigation { get; set; } = new List<Merchant>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MerchantHistory> MerchantHistory { get; set; } = new List<MerchantHistory>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Merchant> MerchantModifiedByNavigation { get; set; } = new List<Merchant>();

    [ForeignKey("ModifiedAt")]
    [InverseProperty("UserModifiedAtNavigation")]
    public virtual LkLocation? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("InverseModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Order> OrderCreatedByNavigation { get; set; } = new List<Order>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Order> OrderModifiedByNavigation { get; set; } = new List<Order>();

    [InverseProperty("User")]
    public virtual ICollection<Payment> Payment { get; set; } = new List<Payment>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<PaymentInstallment> PaymentInstallmentCreatedByNavigation { get; set; } = new List<PaymentInstallment>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<PaymentInstallment> PaymentInstallmentModifiedByNavigation { get; set; } = new List<PaymentInstallment>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<PrePayment> PrePaymentCreatedByNavigation { get; set; } = new List<PrePayment>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<PrePayment> PrePaymentModifiedByNavigation { get; set; } = new List<PrePayment>();

    [InverseProperty("User")]
    public virtual ICollection<PrePayment> PrePaymentUser { get; set; } = new List<PrePayment>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<SmsNotification> SmsNotificationCreatedByNavigation { get; set; } = new List<SmsNotification>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<SmsNotification> SmsNotificationModifiedByNavigation { get; set; } = new List<SmsNotification>();

    [ForeignKey("UserTypeId")]
    [InverseProperty("User")]
    public virtual LkRole UserType { get; set; } = null!;
}
