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

    public byte NotificationTypeId { get; set; }

    [StringLength(60)]
    public string MessageCode { get; set; } = null!;

    [StringLength(250)]
    public string? SubjectEnglish { get; set; }

    [StringLength(250)]
    public string? SubjectArabic { get; set; }

    public string MessageEnglish { get; set; } = null!;

    public string MessageArabic { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    [ForeignKey("NotificationCategoryId")]
    [InverseProperty("LkNotificationTemplate")]
    public virtual LkNotificationCategory? NotificationCategory { get; set; }
}
