using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class Order
{
    [Key]
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid MerchantId { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal TotalAmount { get; set; }

    [Column("ERPReferenceNumber")]
    [StringLength(15)]
    [Unicode(false)]
    public string? ErpreferenceNumber { get; set; }

    public int? CurrencyId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string CheckoutId { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string ExternalRefId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string OrderNumber { get; set; } = null!;

    public int? OrderStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ExpiredAt { get; set; }

    [Column("CheckoutURL")]
    [StringLength(255)]
    [Unicode(false)]
    public string CheckoutUrl { get; set; } = null!;

    public int PaymentOptionId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string MerchantUrlSuccess { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string MerchantUrlFailure { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string MerchantUrlCancel { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public byte CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public byte? ModifiedAt { get; set; }

    [ForeignKey("CurrencyId")]
    [InverseProperty("Order")]
    public virtual LkCurrency? Currency { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("Order")]
    public virtual Merchant Merchant { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();

    [ForeignKey("OrderStatusId")]
    [InverseProperty("Order")]
    public virtual LkOrderStatus? OrderStatus { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<PaymentInstallment> PaymentInstallment { get; set; } = new List<PaymentInstallment>();

    [ForeignKey("PaymentOptionId")]
    [InverseProperty("Order")]
    public virtual LkPaymentOption PaymentOption { get; set; } = null!;
}
