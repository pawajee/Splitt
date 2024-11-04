using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class MerchantAttachment
{
    public Guid Id { get; set; }

    public Guid MerchantRequestId { get; set; }

    [Key]
    public Guid DocumentLibraryId { get; set; }

    public Guid DocumentConfigurationId { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedAt { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("MerchantAttachmentCreatedAtNavigation")]
    public virtual LkLocation CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("MerchantAttachmentCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("DocumentConfigurationId")]
    [InverseProperty("MerchantAttachment")]
    public virtual LkDocumentConfiguration DocumentConfiguration { get; set; } = null!;

    [ForeignKey("DocumentLibraryId")]
    [InverseProperty("MerchantAttachment")]
    public virtual DocumentLibrary DocumentLibrary { get; set; } = null!;

    [ForeignKey("MerchantRequestId")]
    [InverseProperty("MerchantAttachment")]
    public virtual Merchant MerchantRequest { get; set; } = null!;

    [ForeignKey("ModifiedAt")]
    [InverseProperty("MerchantAttachmentModifiedAtNavigation")]
    public virtual LkLocation? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("MerchantAttachmentModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }
}
