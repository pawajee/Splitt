using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class MerchantRequest
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(250)]
    public string BusinessNameEnglish { get; set; } = null!;

    [StringLength(250)]
    public string BusinessNameArabic { get; set; } = null!;

    public int CountryId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? RequestNo { get; set; }

    public int MerchantCategoryId { get; set; }

    public int MerchantRequestStatusId { get; set; }

    public int MerchantBusinessTypeId { get; set; }

    public int MerchantAnnualSalesId { get; set; }

    public int MerchantAverageOrderId { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public byte CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public byte? ModifiedAt { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("MerchantRequest")]
    public virtual Country Country { get; set; } = null!;

    [ForeignKey("CreatedAt")]
    [InverseProperty("MerchantRequestCreatedAtNavigation")]
    public virtual Location CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("MerchantRequestCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("MerchantAnnualSalesId")]
    [InverseProperty("MerchantRequest")]
    public virtual MerchantAnnualSale MerchantAnnualSales { get; set; } = null!;

    [ForeignKey("MerchantAverageOrderId")]
    [InverseProperty("MerchantRequest")]
    public virtual MerchantAverageOrder MerchantAverageOrder { get; set; } = null!;

    [ForeignKey("MerchantBusinessTypeId")]
    [InverseProperty("MerchantRequest")]
    public virtual MerchantBusinessType MerchantBusinessType { get; set; } = null!;

    [ForeignKey("MerchantCategoryId")]
    [InverseProperty("MerchantRequest")]
    public virtual MerchantCategory MerchantCategory { get; set; } = null!;

    [InverseProperty("MerchantRequest")]
    public virtual ICollection<MerchantRequestAttachment> MerchantRequestAttachment { get; set; } = new List<MerchantRequestAttachment>();

    [InverseProperty("MerchantRequest")]
    public virtual ICollection<MerchantRequestHistory> MerchantRequestHistory { get; set; } = new List<MerchantRequestHistory>();

    [ForeignKey("MerchantRequestStatusId")]
    [InverseProperty("MerchantRequest")]
    public virtual MerchantRequestStatus MerchantRequestStatus { get; set; } = null!;

    [InverseProperty("MerchantRequest")]
    public virtual ICollection<MerchantUser> MerchantUser { get; set; } = new List<MerchantUser>();

    [ForeignKey("ModifiedAt")]
    [InverseProperty("MerchantRequestModifiedAtNavigation")]
    public virtual Location? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("MerchantRequestModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }
}
