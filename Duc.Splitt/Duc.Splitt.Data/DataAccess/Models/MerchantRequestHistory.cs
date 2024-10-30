using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class MerchantRequestHistory
{
    [Key]
    public Guid Id { get; set; }

    public Guid MerchantRequestId { get; set; }

    public int RequestStatusId { get; set; }

    [StringLength(2000)]
    public string? Comment { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public byte CreatedAt { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("MerchantRequestHistory")]
    public virtual Location CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("MerchantRequestHistory")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("MerchantRequestId")]
    [InverseProperty("MerchantRequestHistory")]
    public virtual MerchantRequest MerchantRequest { get; set; } = null!;

    [ForeignKey("RequestStatusId")]
    [InverseProperty("MerchantRequestHistory")]
    public virtual RequestStatus RequestStatus { get; set; } = null!;
}
