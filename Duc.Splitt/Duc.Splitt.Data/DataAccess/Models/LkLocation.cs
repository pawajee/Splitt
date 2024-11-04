using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class LkLocation
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TitleEnglish { get; set; } = null!;

    [StringLength(50)]
    public string TitleArabic { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Code { get; set; }

    public byte? SortOrder { get; set; }

    public bool? IsDeleted { get; set; }

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<BackOfficeUser> BackOfficeUserCreatedAtNavigation { get; set; } = new List<BackOfficeUser>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<BackOfficeUser> BackOfficeUserModifiedAtNavigation { get; set; } = new List<BackOfficeUser>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<Customer> CustomerCreatedAtNavigation { get; set; } = new List<Customer>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<Customer> CustomerModifiedAtNavigation { get; set; } = new List<Customer>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<EmailNotification> EmailNotificationCreatedAtNavigation { get; set; } = new List<EmailNotification>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<EmailNotification> EmailNotificationModifiedAtNavigation { get; set; } = new List<EmailNotification>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<MerchantAttachment> MerchantAttachmentCreatedAtNavigation { get; set; } = new List<MerchantAttachment>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<MerchantAttachment> MerchantAttachmentModifiedAtNavigation { get; set; } = new List<MerchantAttachment>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<MerchantContact> MerchantContactCreatedAtNavigation { get; set; } = new List<MerchantContact>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<MerchantContact> MerchantContactModifiedAtNavigation { get; set; } = new List<MerchantContact>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<Merchant> MerchantCreatedAtNavigation { get; set; } = new List<Merchant>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<MerchantHistory> MerchantHistory { get; set; } = new List<MerchantHistory>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<Merchant> MerchantModifiedAtNavigation { get; set; } = new List<Merchant>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<SmsNotification> SmsNotification { get; set; } = new List<SmsNotification>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<User> UserCreatedAtNavigation { get; set; } = new List<User>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<User> UserModifiedAtNavigation { get; set; } = new List<User>();
}
