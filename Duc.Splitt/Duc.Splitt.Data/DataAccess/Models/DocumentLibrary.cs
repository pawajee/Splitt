using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class DocumentLibrary
{
    [Key]
    public Guid Id { get; set; }

    public int? DocumentCategoryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? MineType { get; set; }

    [StringLength(100)]
    public string? FileName { get; set; }

    public byte[]? Attachment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? ModifiedBy { get; set; }

    public byte CreatedAt { get; set; }

    public byte? ModifiedAt { get; set; }

    [ForeignKey("DocumentCategoryId")]
    [InverseProperty("DocumentLibrary")]
    public virtual DocumentCategory? DocumentCategory { get; set; }

    [InverseProperty("DocumentLibrary")]
    public virtual ICollection<MerchantRequestAttachment> MerchantRequestAttachment { get; set; } = new List<MerchantRequestAttachment>();
}
