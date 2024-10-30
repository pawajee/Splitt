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

    [StringLength(250)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LoginId { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? ModifiedBy { get; set; }

    public byte CreatedAt { get; set; }

    public byte? ModifiedAt { get; set; }

    public bool? IsActivated { get; set; }

    public bool? IsSystemAccount { get; set; }

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

    [ForeignKey("ModifiedAt")]
    [InverseProperty("UserModifiedAtNavigation")]
    public virtual Location? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("InverseModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }
}
