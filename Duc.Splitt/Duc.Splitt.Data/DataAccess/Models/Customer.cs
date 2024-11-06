using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Data.DataAccess.Models;

[Index("UserId", Name = "IX_Customer", IsUnique = true)]
public partial class Customer
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? CustomerRegistrationRequestId { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string MobileNo { get; set; } = null!;

    [StringLength(12)]
    [Unicode(false)]
    public string CivilId { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("ERPReferenceNumber")]
    [StringLength(15)]
    [Unicode(false)]
    public string? ErpreferenceNumber { get; set; }

    public int? GenderId { get; set; }

    public int? NationalityId { get; set; }

    public int? MartialStatusId { get; set; }

    [StringLength(500)]
    public string? AdditionalAddress { get; set; }

    public int? Dependents { get; set; }

    public int? EmploymentStatusId { get; set; }

    public int? EmploymentSectorId { get; set; }

    public int? EducationalLevelId { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? Salary { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? OutstandingDebt { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? TotalSpent { get; set; }

    public int? CreditScore { get; set; }

    public int? CustomerStatusId { get; set; }

    [Column("IsUseOtherBNPLApps")]
    public bool? IsUseOtherBnplapps { get; set; }

    [Column("OtherBNPLAppsName")]
    [StringLength(100)]
    public string? OtherBnplappsName { get; set; }

    [StringLength(250)]
    public string? PaciNameEnglish { get; set; }

    [StringLength(250)]
    public string? PaciNameArabic { get; set; }

    public DateOnly? PaciDateOfBirth { get; set; }

    [StringLength(10)]
    public string? PaciGender { get; set; }

    [StringLength(50)]
    public string? PassportNumber { get; set; }

    [StringLength(20)]
    public string? PaciCivilIdSerialNumber { get; set; }

    public DateOnly? PaciCivilIdExpiryDate { get; set; }

    [StringLength(100)]
    public string? PaciGovernorate { get; set; }

    [StringLength(100)]
    public string? PaciArea { get; set; }

    [StringLength(100)]
    public string? PaciBlock { get; set; }

    [StringLength(100)]
    public string? PaciStreet { get; set; }

    [StringLength(100)]
    public string? PaciAvenue { get; set; }

    [StringLength(100)]
    public string? PaciBuildingNumber { get; set; }

    [StringLength(100)]
    public string? PaciBuildingType { get; set; }

    [StringLength(100)]
    public string? PaciFloorNumber { get; set; }

    [StringLength(100)]
    public string? PaciDoorNumber { get; set; }

    [StringLength(50)]
    public string? PaciNationalityEn { get; set; }

    [StringLength(100)]
    public string? PaciNationalityAr { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedAt { get; set; }

    [ForeignKey("CreatedAt")]
    [InverseProperty("CustomerCreatedAtNavigation")]
    public virtual LkLocation CreatedAtNavigation { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("CustomerCreatedByNavigation")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("CustomerRegistrationRequestId")]
    [InverseProperty("Customer")]
    public virtual CustomerRegistrationRequest? CustomerRegistrationRequest { get; set; }

    [ForeignKey("CustomerStatusId")]
    [InverseProperty("Customer")]
    public virtual LkCustomerStatus? CustomerStatus { get; set; }

    [ForeignKey("EducationalLevelId")]
    [InverseProperty("Customer")]
    public virtual LkEducationalLevel? EducationalLevel { get; set; }

    [ForeignKey("EmploymentSectorId")]
    [InverseProperty("Customer")]
    public virtual LkEmploymentSector? EmploymentSector { get; set; }

    [ForeignKey("EmploymentStatusId")]
    [InverseProperty("Customer")]
    public virtual LkEmploymentStatus? EmploymentStatus { get; set; }

    [ForeignKey("GenderId")]
    [InverseProperty("Customer")]
    public virtual LkGender? Gender { get; set; }

    [ForeignKey("MartialStatusId")]
    [InverseProperty("Customer")]
    public virtual LkMartialStatus? MartialStatus { get; set; }

    [ForeignKey("ModifiedAt")]
    [InverseProperty("CustomerModifiedAtNavigation")]
    public virtual LkLocation? ModifiedAtNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("CustomerModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("NationalityId")]
    [InverseProperty("Customer")]
    public virtual LkNationality? Nationality { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("CustomerUser")]
    public virtual User User { get; set; } = null!;
}
