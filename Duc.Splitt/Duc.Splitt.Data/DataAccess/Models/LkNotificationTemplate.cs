using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class LkNotificationTemplate
{
    [Key]
    public int Id { get; set; }

    public int? NotificationCategoryId { get; set; }

    public int NotificationTypeId { get; set; }

    [StringLength(60)]
    public string MessageCode { get; set; } = null!;

    [StringLength(250)]
    public string? SubjectEnglish { get; set; }

    [StringLength(250)]
    public string? SubjectArabic { get; set; }

    public string MessageEnglish { get; set; } = null!;

    public string MessageArabic { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    [InverseProperty("NotificationTemplate")]
    public virtual ICollection<EmailNotification> EmailNotification { get; set; } = new List<EmailNotification>();

    [ForeignKey("NotificationCategoryId")]
    [InverseProperty("LkNotificationTemplate")]
    public virtual LkNotificationCategory? NotificationCategory { get; set; }

    [ForeignKey("NotificationTypeId")]
    [InverseProperty("LkNotificationTemplate")]
    public virtual LkNotificationType NotificationType { get; set; } = null!;

    [InverseProperty("NotificationTemplate")]
    public virtual ICollection<PushNotification> PushNotification { get; set; } = new List<PushNotification>();

    [InverseProperty("NotificationTemplate")]
    public virtual ICollection<SmsNotification> SmsNotification { get; set; } = new List<SmsNotification>();
}
