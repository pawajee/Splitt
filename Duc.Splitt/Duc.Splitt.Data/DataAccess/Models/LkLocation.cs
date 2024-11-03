using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class LkLocation
{
    [Key]
    public byte Id { get; set; }

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
    public virtual ICollection<ConsumerUser> ConsumerUserCreatedAtNavigation { get; set; } = new List<ConsumerUser>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<ConsumerUser> ConsumerUserModifiedAtNavigation { get; set; } = new List<ConsumerUser>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<LkNotificationTemplate> LkNotificationTemplateCreatedAtNavigation { get; set; } = new List<LkNotificationTemplate>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<LkNotificationTemplate> LkNotificationTemplateModifiedAtNavigation { get; set; } = new List<LkNotificationTemplate>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<MerchantAttachment> MerchantAttachmentCreatedAtNavigation { get; set; } = new List<MerchantAttachment>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<MerchantAttachment> MerchantAttachmentModifiedAtNavigation { get; set; } = new List<MerchantAttachment>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<Merchant> MerchantCreatedAtNavigation { get; set; } = new List<Merchant>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<MerchantHistory> MerchantHistory { get; set; } = new List<MerchantHistory>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<Merchant> MerchantModifiedAtNavigation { get; set; } = new List<Merchant>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<MerchantUser> MerchantUserCreatedAtNavigation { get; set; } = new List<MerchantUser>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<MerchantUser> MerchantUserModifiedAtNavigation { get; set; } = new List<MerchantUser>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<User> UserCreatedAtNavigation { get; set; } = new List<User>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<User> UserModifiedAtNavigation { get; set; } = new List<User>();
}
