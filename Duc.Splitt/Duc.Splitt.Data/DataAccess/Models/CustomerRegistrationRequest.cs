﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class CustomerRegistrationRequest
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(12)]
    [Unicode(false)]
    public string CivilId { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string MobileNo { get; set; } = null!;

    public Guid? OtpRequestId { get; set; }

    [Column("PACINumberofAttempts")]
    public int? PacinumberofAttempts { get; set; }

    [Column("PACIVerifiedOn", TypeName = "datetime")]
    public DateTime? PaciverifiedOn { get; set; }

    public bool? IsBlocked { get; set; }

    public int? CustomerRegistrationStatusId { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedAt { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("CustomerRegistrationRequestCreatedAtNavigation")]
    public virtual LkLocation CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("CustomerRegistrationRequestCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [InverseProperty("CustomerRegistrationRequest")]
    public virtual ICollection<Customer> Customer { get; set; } = new List<Customer>();

    [ForeignKey("CustomerRegistrationStatusId")]
    [InverseProperty("CustomerRegistrationRequest")]
    public virtual LkCustomerRegistrationStatus? CustomerRegistrationStatus { get; set; }

    [InverseProperty("CustomerRegistrationRequest")]
    public virtual ICollection<MidRequestLog> MidRequestLog { get; set; } = new List<MidRequestLog>();

    [ForeignKey("ModifiedAt")]
    [InverseProperty("CustomerRegistrationRequestModifiedAtNavigation")]
    public virtual LkLocation? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("CustomerRegistrationRequestModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("OtpRequestId")]
    [InverseProperty("CustomerRegistrationRequest")]
    public virtual OtpRequest? OtpRequest { get; set; }
}
