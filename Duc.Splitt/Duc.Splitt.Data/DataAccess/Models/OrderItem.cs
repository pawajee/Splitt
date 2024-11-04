using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class OrderItem
{
    [Key]
    public Guid Id { get; set; }

    [Column("ERPReferenceNumber")]
    [StringLength(15)]
    [Unicode(false)]
    public string? ErpreferenceNumber { get; set; }

    public Guid? OrderId { get; set; }

    [StringLength(100)]
    public string? ItemName { get; set; }

    [StringLength(244)]
    public string? ItemDescription { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? Amount { get; set; }

    [StringLength(100)]
    public string? ExternalRefId { get; set; }

    [Column("ItemImageURL")]
    [StringLength(254)]
    public string? ItemImageUrl { get; set; }

    [Column("ProductURL")]
    [StringLength(254)]
    public string? ProductUrl { get; set; }

    [StringLength(100)]
    public string? BrandName { get; set; }

    [Column("SKU")]
    [StringLength(50)]
    public string? Sku { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public byte CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public byte? ModifiedAt { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItem")]
    public virtual Order? Order { get; set; }
}
