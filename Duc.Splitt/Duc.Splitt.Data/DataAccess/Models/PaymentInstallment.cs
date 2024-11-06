using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class PaymentInstallment
{
    [Key]
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string InstallmentAmount { get; set; } = null!;

    [StringLength(12)]
    [Unicode(false)]
    public string DueAmount { get; set; } = null!;

    public int? DueDate { get; set; }

    [Column("ERPReferenceNumber")]
    [StringLength(15)]
    [Unicode(false)]
    public string? ErpreferenceNumber { get; set; }

    public int InstallmentTypeId { get; set; }

    public int? PaymentStatusId { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public byte CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public byte? ModifiedAt { get; set; }

    [ForeignKey("InstallmentTypeId")]
    [InverseProperty("PaymentInstallment")]
    public virtual LkInstallmentType InstallmentType { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("PaymentInstallment")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("PaymentStatusId")]
    [InverseProperty("PaymentInstallment")]
    public virtual LkPaymentStatus? PaymentStatus { get; set; }
}
