using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class OtpRequest
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string MobileNo { get; set; } = null!;

    [StringLength(6)]
    [Unicode(false)]
    public string Otp { get; set; } = null!;

    public int OtpPurposeId { get; set; }

    public int? NumberofAttempts { get; set; }

    public bool? IsUsed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ExpiredOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? VerifiedOn { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Status { get; set; }

    [StringLength(2000)]
    public string? Comments { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public byte CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public byte? ModifiedAt { get; set; }

    [InverseProperty("OtpRequest")]
    public virtual ICollection<CustomerRegistrationRequest> CustomerRegistrationRequest { get; set; } = new List<CustomerRegistrationRequest>();

    [ForeignKey("OtpPurposeId")]
    [InverseProperty("OtpRequest")]
    public virtual LkOtpPurpose OtpPurpose { get; set; } = null!;
}
