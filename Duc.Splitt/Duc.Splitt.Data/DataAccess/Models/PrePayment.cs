using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class PrePayment
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int PaymentRequestTypeId { get; set; }

    public Guid PaymentInstallmentId { get; set; }

    [Column(TypeName = "decimal(5, 3)")]
    public decimal Amount { get; set; }

    public int PaymentBrandId { get; set; }

    public int PaymentStatusId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CurrencyId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? CheckoutId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ResultCode { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? ResultDescription { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public string? PaymentGatewayRequestPayload { get; set; }

    public string? PaymentGatewayResponsePayload { get; set; }

    public int? NosofPendingPaymentReTried { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public byte CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public byte? ModifiedAt { get; set; }

    [InverseProperty("PrePayment")]
    public virtual ICollection<Payment> Payment { get; set; } = new List<Payment>();

    [ForeignKey("PaymentBrandId")]
    [InverseProperty("PrePayment")]
    public virtual LkPaymentBrandType PaymentBrand { get; set; } = null!;

    [ForeignKey("PaymentRequestTypeId")]
    [InverseProperty("PrePayment")]
    public virtual LkPaymentRequestType PaymentRequestType { get; set; } = null!;

    [ForeignKey("PaymentStatusId")]
    [InverseProperty("PrePayment")]
    public virtual LkPaymentStatus PaymentStatus { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("PrePayment")]
    public virtual User User { get; set; } = null!;
}
