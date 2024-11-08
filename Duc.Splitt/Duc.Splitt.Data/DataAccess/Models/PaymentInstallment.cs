﻿using System;
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

    [Column(TypeName = "decimal(10, 3)")]
    public decimal InstallmentAmount { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal DueAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DueDate { get; set; }

    [Column("ERPReferenceNumber")]
    [StringLength(15)]
    [Unicode(false)]
    public string? ErpreferenceNumber { get; set; }

    public int InstallmentTypeId { get; set; }

    public int? PaymentStatusId { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedAt { get; set; }

    public Guid ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedOn { get; set; }

    public int ModifiedAt { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("PaymentInstallmentCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("InstallmentTypeId")]
    [InverseProperty("PaymentInstallment")]
    public virtual LkInstallmentType InstallmentType { get; set; } = null!;

    [ForeignKey("ModifiedBy")]
    [InverseProperty("PaymentInstallmentModifiedByNavigation")]
    public virtual User ModifiedByNavigation { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("PaymentInstallment")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("PaymentStatusId")]
    [InverseProperty("PaymentInstallment")]
    public virtual LkPaymentStatus? PaymentStatus { get; set; }
}
