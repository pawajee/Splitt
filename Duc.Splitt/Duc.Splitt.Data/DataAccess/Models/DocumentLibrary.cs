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
    public virtual LkDocumentCategory? DocumentCategory { get; set; }

    [InverseProperty("DocumentLibrary")]
    public virtual MerchantAttachment? MerchantAttachment { get; set; }

    [InverseProperty("PaymentRecepitDocumentLibrary")]
    public virtual ICollection<Payment> Payment { get; set; } = new List<Payment>();
}
