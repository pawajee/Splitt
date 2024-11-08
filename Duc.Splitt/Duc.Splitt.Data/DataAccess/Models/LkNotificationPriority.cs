﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class LkNotificationPriority
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string TitleEnglish { get; set; } = null!;

    [StringLength(50)]
    public string TitleArabic { get; set; } = null!;

    [StringLength(50)]
    public string? Code { get; set; }

    public byte? SortOrder { get; set; }

    public bool? IsDeleted { get; set; }

    [InverseProperty("Priority")]
    public virtual ICollection<EmailNotification> EmailNotification { get; set; } = new List<EmailNotification>();

    [InverseProperty("Priority")]
    public virtual ICollection<PushNotification> PushNotification { get; set; } = new List<PushNotification>();

    [InverseProperty("Priority")]
    public virtual ICollection<SmsNotification> SmsNotification { get; set; } = new List<SmsNotification>();
}
