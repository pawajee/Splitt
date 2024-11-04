using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

[Index("UserId", Name = "IX_MerchantContact", IsUnique = true)]
public partial class MerchantContact
{
    [Key]
    public Guid Id { get; set; }

    public Guid MerchantRequestId { get; set; }

    public Guid UserId { get; set; }

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

    public int CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedAt { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("MerchantContactCreatedAtNavigation")]
    public virtual LkLocation CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("MerchantContactCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("MerchantRequestId")]
    [InverseProperty("MerchantContact")]
    public virtual Merchant MerchantRequest { get; set; } = null!;

    [ForeignKey("ModifiedAt")]
    [InverseProperty("MerchantContactModifiedAtNavigation")]
    public virtual LkLocation? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("MerchantContactModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("MerchantContactUser")]
    public virtual User User { get; set; } = null!;
}
