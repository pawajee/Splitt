﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

public partial class RequestStatus
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

    [InverseProperty("RequestStatus")]
    public virtual ICollection<MerchantRequest> MerchantRequest { get; set; } = new List<MerchantRequest>();

    [InverseProperty("RequestStatus")]
    public virtual ICollection<MerchantRequestHistory> MerchantRequestHistory { get; set; } = new List<MerchantRequestHistory>();
}