using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class LkDocumentConfiguration
{
    [Key]
    public Guid Id { get; set; }

    public int DocumentCategoryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TitleEnglish { get; set; } = null!;

    [StringLength(50)]
    public string TitleArabic { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string? DescriptionEnglish { get; set; }

    [StringLength(250)]
    public string? DescriptionArabic { get; set; }

    public bool IsRequired { get; set; }

    public int MaxFileSizeKb { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string SupportedMineType { get; set; } = null!;

    [StringLength(50)]
    public string? Code { get; set; }

    public byte? SortOrder { get; set; }

    public bool? IsDeleted { get; set; }

    [ForeignKey("DocumentCategoryId")]
    [InverseProperty("LkDocumentConfiguration")]
    public virtual LkDocumentCategory DocumentCategory { get; set; } = null!;

    [InverseProperty("DocumentConfiguration")]
    public virtual ICollection<MerchantAttachment> MerchantAttachment { get; set; } = new List<MerchantAttachment>();
}
