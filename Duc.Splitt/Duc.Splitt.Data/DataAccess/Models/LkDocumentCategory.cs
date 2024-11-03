using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class LkDocumentCategory
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TitleEnglish { get; set; } = null!;

    [StringLength(50)]
    public string TitleArabic { get; set; } = null!;

    [Column("BaseURL")]
    [StringLength(250)]
    [Unicode(false)]
    public string? BaseUrl { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Code { get; set; }

    public int? SortOrder { get; set; }

    public bool? IsDeleted { get; set; }

    [InverseProperty("DocumentCategory")]
    public virtual ICollection<DocumentLibrary> DocumentLibrary { get; set; } = new List<DocumentLibrary>();

    [InverseProperty("DocumentCategory")]
    public virtual ICollection<LkDocumentConfiguration> LkDocumentConfiguration { get; set; } = new List<LkDocumentConfiguration>();
}
