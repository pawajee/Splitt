using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class ConsumerUser
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    [StringLength(250)]
    public string? NameEnglish { get; set; }

    [StringLength(250)]
    public string? NameArabic { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string MobileNo { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public byte CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public byte? ModifiedAt { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("ConsumerUserCreatedAtNavigation")]
    public virtual LkLocation CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("ConsumerUserCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("ModifiedAt")]
    [InverseProperty("ConsumerUserModifiedAtNavigation")]
    public virtual LkLocation? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("ConsumerUserModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ConsumerUserUser")]
    public virtual User User { get; set; } = null!;
}
