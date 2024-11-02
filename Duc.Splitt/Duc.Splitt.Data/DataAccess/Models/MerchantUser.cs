using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class MerchantUser
{
    [Key]
    public Guid Id { get; set; }

    public Guid MerchantRequestId { get; set; }

    public Guid? UserId { get; set; }

    [StringLength(250)]
    public string NameEnglish { get; set; } = null!;

    [StringLength(250)]
    public string NameArabic { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string BusinessEmail { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string MobileNo { get; set; } = null!;

    public bool IsPrimary { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public byte CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public byte? ModifiedAt { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("MerchantUserCreatedAtNavigation")]
    public virtual Location CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("MerchantUserCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("MerchantRequestId")]
    [InverseProperty("MerchantUser")]
    public virtual MerchantRequest MerchantRequest { get; set; } = null!;

    [ForeignKey("ModifiedAt")]
    [InverseProperty("MerchantUserModifiedAtNavigation")]
    public virtual Location? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("MerchantUserModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("MerchantUserUser")]
    public virtual User? User { get; set; }
}
