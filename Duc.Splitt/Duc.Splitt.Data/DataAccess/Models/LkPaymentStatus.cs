using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class LkPaymentStatus
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TitleEnglish { get; set; } = null!;

    [StringLength(50)]
    public string TitleArabic { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    public byte SortOrder { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("PaymentStatus")]
    public virtual ICollection<Payment> Payment { get; set; } = new List<Payment>();

    [InverseProperty("PaymentStatus")]
    public virtual ICollection<PaymentInstallment> PaymentInstallment { get; set; } = new List<PaymentInstallment>();

    [InverseProperty("PaymentStatus")]
    public virtual ICollection<PrePayment> PrePayment { get; set; } = new List<PrePayment>();
}
