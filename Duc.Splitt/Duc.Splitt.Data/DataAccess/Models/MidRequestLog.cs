using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class MidRequestLog
{
    [Key]
    public int Id { get; set; }

    public Guid? CustomerRegistrationRequestId { get; set; }

    [Column("DSPRefId")]
    [StringLength(50)]
    public string DsprefId { get; set; } = null!;

    public int MidRequestStatusId { get; set; }

    [Column("MIDPayloadRequest")]
    public string? MidpayloadRequest { get; set; }

    [Column("MIDPayloadResponse")]
    public string? MidpayloadResponse { get; set; }

    public int MidRequestTypeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public int CreatedAt { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? ModifiedOn { get; set; }

    public Guid ModifiedBy { get; set; }

    public int ModifiedAt { get; set; }

    public bool? IsDeleted { get; set; }

    [ForeignKey("CustomerRegistrationRequestId")]
    [InverseProperty("MidRequestLog")]
    public virtual CustomerRegistrationRequest? CustomerRegistrationRequest { get; set; }

    [ForeignKey("MidRequestStatusId")]
    [InverseProperty("MidRequestLog")]
    public virtual LkMidRequestStatus MidRequestStatus { get; set; } = null!;

    [ForeignKey("MidRequestTypeId")]
    [InverseProperty("MidRequestLog")]
    public virtual LkMidRequestType MidRequestType { get; set; } = null!;
}
