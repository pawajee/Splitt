using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class EmailNotification
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    [StringLength(50)]
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

    public int? ModifiedAt { get; set; }

    public int? NotificationCategoryId { get; set; }

    [StringLength(50)]
    public string? ReferenceId { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("EmailNotificationCreatedAtNavigation")]
    public virtual LkLocation CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("EmailNotificationCreatedByNavigation")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("LanguageId")]
    [InverseProperty("EmailNotification")]
    public virtual LkLanguage Language { get; set; } = null!;

    [ForeignKey("ModifiedAt")]
    [InverseProperty("EmailNotificationModifiedAtNavigation")]
    public virtual LkLocation? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("EmailNotificationModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("NotificationCategoryId")]
    [InverseProperty("EmailNotification")]
    public virtual LkNotificationCategory? NotificationCategory { get; set; }

    [ForeignKey("PriorityId")]
    [InverseProperty("EmailNotification")]
    public virtual LkNotificationPriority? Priority { get; set; }
}
