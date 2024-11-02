using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class Location
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
    public virtual ICollection<MerchantRequestAttachment> MerchantRequestAttachmentCreatedAtNavigation { get; set; } = new List<MerchantRequestAttachment>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<MerchantRequestAttachment> MerchantRequestAttachmentModifiedAtNavigation { get; set; } = new List<MerchantRequestAttachment>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<MerchantRequest> MerchantRequestCreatedAtNavigation { get; set; } = new List<MerchantRequest>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<MerchantRequestHistory> MerchantRequestHistory { get; set; } = new List<MerchantRequestHistory>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<MerchantRequest> MerchantRequestModifiedAtNavigation { get; set; } = new List<MerchantRequest>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<MerchantUser> MerchantUserCreatedAtNavigation { get; set; } = new List<MerchantUser>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<MerchantUser> MerchantUserModifiedAtNavigation { get; set; } = new List<MerchantUser>();

    [InverseProperty("CreatedAtNavigation")]
    public virtual ICollection<User> UserCreatedAtNavigation { get; set; } = new List<User>();

    [InverseProperty("ModifiedAtNavigation")]
    public virtual ICollection<User> UserModifiedAtNavigation { get; set; } = new List<User>();
}
