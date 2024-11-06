namespace Duc.Splitt.MIDIntegrationService
{
    public class CallbackResponse
    {
        public MIDAuthSignResponse MIDAuthSignResponse { get; set; }
    }
    public class MIDAuthSignResponse
    {
        public RequestDetails RequestDetails { get; set; }
        public ResultDetails ResultDetails { get; set; }
        public PersonalData PersonalData { get; set; }
    }

    public class RequestDetails
    {
        public string RequestID { get; set; }
        public RequestType RequestType { get; set; }
        public string ServiceProviderId { get; set; }
        public string AdditionalData { get; set; }
        public string Challenge { get; set; }
        public string CivilNo { get; set; }
    }
    public class ResultDetails
    {
        public string ResultCode { get; set; }
        public string UserAction { get; set; }

        public string UserCivilNo { get; set; }
        public string UserCertificate { get; set; }
        public byte[] SigningData { get; set; }
        public string SignatureData { get; set; }
        public string HashAlgorithm { get; set; }
        public SigningDatatype SigningDatatype { get; set; }
        public DateTime TransactionDate { get; set; }
    }
    public class PersonalData
    {
        public string CivilID { get; set; }
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string NationalityEn { get; set; }
        public string NationalityAr { get; set; }
        public byte[] NationalityFlag { get; set; }
        public string BloodGroup { get; set; }
        public string Photo { get; set; }

        public string PassportNumber { get; set; }

        public string CardSerial { get; set; }
        public string CardExpiryDate { get; set; }
        // public GovIdData GovData { get; set; }
        // public BusinessIdData BusinessData { get; set; }
        public Address Address { get; set; }


    }

    public class GovIdData
    {
        public DateTime CivilIdExpiryDate { get; set; }
        public string MOIReference { get; set; }
        public string SponsorName { get; set; }
    }

    public class BusinessIdData
    {
        public string OrganizationNameAr { get; set; }
        public string OrganizationNameEn { get; set; }
        public string OrganizationUnitNameAr { get; set; }
        public string OrganizationUnitNameEn { get; set; }
        public string JobTitleAr { get; set; }
        public string JobTitleEn { get; set; }
    }

    public class Address
    {
        public string Governerate { get; set; }
        public string Area { get; set; }
        public string PaciBuildingNumber { get; set; }
        public string BuildingType { get; set; }
        public string FloorNumber { get; set; }
        public string BuildingNumber { get; set; }
        public string BlockNumber { get; set; }
        public string UnitNumber { get; set; }
        public string StreetName { get; set; }
        public string UnitType { get; set; }

    }
    public enum RequestType
    {
        Authentication = 1,
        Signing = 2
    }

    public enum SigningDatatype
    {
        PKCS7 = 1,
        PKCS1 = 2
    }
}
