using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class SmsNotification
{
    [Key]
    public int Id { get; set; }

    [StringLength(500)]
    public string Message { get; set; } = null!;

    [StringLength(20)]
    public string? Recipient { get; set; }

    public int LanguageId { get; set; }

    public byte NotificationStatusId { get; set; }

    public int? PriorityId { get; set; }

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? ModifiedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? ModifiedBy { get; set; }

    public int CreatedAt { get; set; }

    public byte? ModifiedAt { get; set; }

    [StringLength(50)]
    public string? ReferenceId { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("SmsNotification")]
    public virtual LkLocation CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("LanguageId")]
    [InverseProperty("SmsNotification")]
    public virtual LkLanguage Language { get; set; } = null!;

    [ForeignKey("PriorityId")]
    [InverseProperty("SmsNotification")]
    public virtual LkNotificationPriority? Priority { get; set; }
}
