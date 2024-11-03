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

    [StringLength(60)]
    public string MessageCode { get; set; } = null!;

    [StringLength(250)]
    public string? SubjectEnglish { get; set; }

    [StringLength(250)]
    public string? SubjectArabic { get; set; }

    public string MessageEnglish { get; set; } = null!;

    public string MessageArabic { get; set; } = null!;

    public byte NotificationTypeId { get; set; }

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? ModifiedBy { get; set; }

    public byte CreatedAt { get; set; }

    public byte? ModifiedAt { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("LkNotificationTemplateCreatedAtNavigation")]
    public virtual LkLocation CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("ModifiedAt")]
    [InverseProperty("LkNotificationTemplateModifiedAtNavigation")]
    public virtual LkLocation? ModifiedAtNavigation { get; set; }
}
