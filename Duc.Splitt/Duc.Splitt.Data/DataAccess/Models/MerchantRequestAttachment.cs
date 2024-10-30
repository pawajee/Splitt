using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class MerchantRequestAttachment
{
    [Key]
    public Guid Id { get; set; }

    public Guid MerchantRequestId { get; set; }

    public Guid DocumentLibraryId { get; set; }

    public Guid DocumentConfigurationId { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public byte CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public byte? ModifiedAt { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("MerchantRequestAttachmentCreatedAtNavigation")]
    public virtual Location CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("MerchantRequestAttachmentCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("DocumentConfigurationId")]
    [InverseProperty("MerchantRequestAttachment")]
    public virtual DocumentConfiguration DocumentConfiguration { get; set; } = null!;

    [ForeignKey("DocumentLibraryId")]
    [InverseProperty("MerchantRequestAttachment")]
    public virtual DocumentLibrary DocumentLibrary { get; set; } = null!;

    [ForeignKey("MerchantRequestId")]
    [InverseProperty("MerchantRequestAttachment")]
    public virtual MerchantRequest MerchantRequest { get; set; } = null!;

    [ForeignKey("ModifiedAt")]
    [InverseProperty("MerchantRequestAttachmentModifiedAtNavigation")]
    public virtual Location? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("MerchantRequestAttachmentModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }
}
