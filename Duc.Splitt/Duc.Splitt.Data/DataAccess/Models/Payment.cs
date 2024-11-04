using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class Payment
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid PrePaymentId { get; set; }

    [Column("ERPReferenceNumber")]
    [StringLength(15)]
    [Unicode(false)]
    public string? ErpreferenceNumber { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string PaymentType { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string PaymentBrand { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Currency { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string CheckoutId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ResultCode { get; set; } = null!;

    public int PaymentStatusId { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string ResultDescription { get; set; } = null!;

    [Column("ResponceURL")]
    [StringLength(500)]
    [Unicode(false)]
    public string ResponceUrl { get; set; } = null!;

    [Unicode(false)]
    public string? Responce { get; set; }

    [Column("IP")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Ip { get; set; }

    [Column("IPCountry")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Ipcountry { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedAt { get; set; }

    public Guid? PaymentRecepitDocumentLibraryId { get; set; }

    [ForeignKey("PaymentRecepitDocumentLibraryId")]
    [InverseProperty("Payment")]
    public virtual DocumentLibrary? PaymentRecepitDocumentLibrary { get; set; }

    [ForeignKey("PaymentStatusId")]
    [InverseProperty("Payment")]
    public virtual LkPaymentStatus PaymentStatus { get; set; } = null!;

    [ForeignKey("PrePaymentId")]
    [InverseProperty("Payment")]
    public virtual PrePayment PrePayment { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Payment")]
    public virtual User User { get; set; } = null!;
}
