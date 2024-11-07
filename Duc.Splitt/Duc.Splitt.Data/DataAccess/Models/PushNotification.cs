using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class PushNotification
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Title { get; set; } = null!;

    [StringLength(4000)]
    public string Body { get; set; } = null!;

    public byte DeviceTypeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DeviceId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? DeviceToken { get; set; }

    public int LanguageId { get; set; }

    public int NotificationStatusId { get; set; }

    public int? PriorityId { get; set; }

    public string? Details { get; set; }

    public int NotificationTemplateId { get; set; }

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? ModifiedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? ModifiedBy { get; set; }

    public byte CreatedAt { get; set; }

    public byte? ModifiedAt { get; set; }

    [StringLength(50)]
    public string? ReferenceId { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? ReadOn { get; set; }

    public Guid? TokenId { get; set; }

    [ForeignKey("NotificationStatusId")]
    [InverseProperty("PushNotification")]
    public virtual LkNotificationStatus NotificationStatus { get; set; } = null!;

    [ForeignKey("NotificationTemplateId")]
    [InverseProperty("PushNotification")]
    public virtual LkNotificationTemplate NotificationTemplate { get; set; } = null!;

    [ForeignKey("PriorityId")]
    [InverseProperty("PushNotification")]
    public virtual LkNotificationPriority? Priority { get; set; }
}
