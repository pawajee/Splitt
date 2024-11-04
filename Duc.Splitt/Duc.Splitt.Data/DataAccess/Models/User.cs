﻿using System;
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
    public virtual BackOfficeUser? BackOfficeUserUser { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<ConsumerUser> ConsumerUserCreatedByNavigation { get; set; } = new List<ConsumerUser>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<ConsumerUser> ConsumerUserModifiedByNavigation { get; set; } = new List<ConsumerUser>();

    [InverseProperty("User")]
    public virtual ConsumerUser? ConsumerUserUser { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("UserCreatedAtNavigation")]
    public virtual LkLocation CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("InverseCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<User> InverseModifiedByNavigation { get; set; } = new List<User>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MerchantAttachment> MerchantAttachmentCreatedByNavigation { get; set; } = new List<MerchantAttachment>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<MerchantAttachment> MerchantAttachmentModifiedByNavigation { get; set; } = new List<MerchantAttachment>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Merchant> MerchantCreatedByNavigation { get; set; } = new List<Merchant>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MerchantHistory> MerchantHistory { get; set; } = new List<MerchantHistory>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Merchant> MerchantModifiedByNavigation { get; set; } = new List<Merchant>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MerchantUser> MerchantUserCreatedByNavigation { get; set; } = new List<MerchantUser>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<MerchantUser> MerchantUserModifiedByNavigation { get; set; } = new List<MerchantUser>();

    [InverseProperty("User")]
    public virtual MerchantUser? MerchantUserUser { get; set; }

    [ForeignKey("ModifiedAt")]
    [InverseProperty("UserModifiedAtNavigation")]
    public virtual LkLocation? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("InverseModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("UserTypeId")]
    [InverseProperty("User")]
    public virtual LkRole UserType { get; set; } = null!;
}
