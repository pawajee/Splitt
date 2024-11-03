using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class MerchantHistory
{
    [Key]
    public Guid Id { get; set; }

    public Guid MerchantRequestId { get; set; }

    public int MerchantRequestStatusId { get; set; }

    [StringLength(2000)]
    public string? Comment { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public byte CreatedAt { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("MerchantHistory")]
    public virtual LkLocation CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("MerchantHistory")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("MerchantRequestId")]
    [InverseProperty("MerchantHistory")]
    public virtual Merchant MerchantRequest { get; set; } = null!;

    [ForeignKey("MerchantRequestStatusId")]
    [InverseProperty("MerchantHistory")]
    public virtual LkMerchantStatus MerchantRequestStatus { get; set; } = null!;
}
