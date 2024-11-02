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

    public byte CreatedAt { get; set; }

    public byte? ModifiedAt { get; set; }

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
    public virtual ICollection<BackOfficeUser> BackOfficeUserUser { get; set; } = new List<BackOfficeUser>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<ConsumerUser> ConsumerUserCreatedByNavigation { get; set; } = new List<ConsumerUser>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<ConsumerUser> ConsumerUserModifiedByNavigation { get; set; } = new List<ConsumerUser>();

    [InverseProperty("User")]
    public virtual ICollection<ConsumerUser> ConsumerUserUser { get; set; } = new List<ConsumerUser>();

    [ForeignKey("CreatedAt")]
    [InverseProperty("UserCreatedAtNavigation")]
    public virtual Location CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("InverseCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<User> InverseModifiedByNavigation { get; set; } = new List<User>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MerchantRequestAttachment> MerchantRequestAttachmentCreatedByNavigation { get; set; } = new List<MerchantRequestAttachment>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<MerchantRequestAttachment> MerchantRequestAttachmentModifiedByNavigation { get; set; } = new List<MerchantRequestAttachment>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MerchantRequest> MerchantRequestCreatedByNavigation { get; set; } = new List<MerchantRequest>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MerchantRequestHistory> MerchantRequestHistory { get; set; } = new List<MerchantRequestHistory>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<MerchantRequest> MerchantRequestModifiedByNavigation { get; set; } = new List<MerchantRequest>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MerchantUser> MerchantUserCreatedByNavigation { get; set; } = new List<MerchantUser>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<MerchantUser> MerchantUserModifiedByNavigation { get; set; } = new List<MerchantUser>();

    [InverseProperty("User")]
    public virtual ICollection<MerchantUser> MerchantUserUser { get; set; } = new List<MerchantUser>();

    [ForeignKey("ModifiedAt")]
    [InverseProperty("UserModifiedAtNavigation")]
    public virtual Location? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("InverseModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("UserTypeId")]
    [InverseProperty("User")]
    public virtual UserType UserType { get; set; } = null!;
}
