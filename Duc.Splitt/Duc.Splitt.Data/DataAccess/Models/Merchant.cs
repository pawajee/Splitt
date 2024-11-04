using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class Merchant
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(250)]
    public string BusinessNameEnglish { get; set; } = null!;

    [StringLength(250)]
    public string BusinessNameArabic { get; set; } = null!;

    [Column("ERPReferenceNumber")]
    [StringLength(15)]
    [Unicode(false)]
    public string? ErpreferenceNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string BusinessEmail { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string MobileNo { get; set; } = null!;

    public int CountryId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? RequestNo { get; set; }

    public int MerchantCategoryId { get; set; }

    public int MerchantStatusId { get; set; }

    public int MerchantBusinessTypeId { get; set; }

    public int MerchantAnnualSalesId { get; set; }

    public int MerchantAverageOrderId { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedAt { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("Merchant")]
    public virtual LkCountry Country { get; set; } = null!;

    [ForeignKey("CreatedAt")]
    [InverseProperty("MerchantCreatedAtNavigation")]
    public virtual LkLocation CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("MerchantCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("MerchantAnnualSalesId")]
    [InverseProperty("Merchant")]
    public virtual LkMerchantAnnualSale MerchantAnnualSales { get; set; } = null!;

    [InverseProperty("MerchantRequest")]
    public virtual ICollection<MerchantAttachment> MerchantAttachment { get; set; } = new List<MerchantAttachment>();

    [ForeignKey("MerchantAverageOrderId")]
    [InverseProperty("Merchant")]
    public virtual LkMerchantAverageOrder MerchantAverageOrder { get; set; } = null!;

    [ForeignKey("MerchantBusinessTypeId")]
    [InverseProperty("Merchant")]
    public virtual LkMerchantBusinessType MerchantBusinessType { get; set; } = null!;

    [ForeignKey("MerchantCategoryId")]
    [InverseProperty("Merchant")]
    public virtual LkMerchantCategory MerchantCategory { get; set; } = null!;

    [InverseProperty("MerchantRequest")]
    public virtual ICollection<MerchantContact> MerchantContact { get; set; } = new List<MerchantContact>();

    [InverseProperty("MerchantRequest")]
    public virtual ICollection<MerchantHistory> MerchantHistory { get; set; } = new List<MerchantHistory>();

    [ForeignKey("MerchantStatusId")]
    [InverseProperty("Merchant")]
    public virtual LkMerchantStatus MerchantStatus { get; set; } = null!;

    [ForeignKey("ModifiedAt")]
    [InverseProperty("MerchantModifiedAtNavigation")]
    public virtual LkLocation? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("MerchantModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }

    [InverseProperty("Merchant")]
    public virtual ICollection<Order> Order { get; set; } = new List<Order>();
}
