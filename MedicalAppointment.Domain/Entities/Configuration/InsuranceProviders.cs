
using MedicalAppointment.Domain.Base;

namespace MedicalAppointment.Domain.Entities.Configuration
{
    public class InsuranceProviders : BaseEntity
    {
        public int InsuranceProviderID { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string? WebSite { get; set; }
        public string Address {  get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipeCode { get; set; }
        public string CoverageDetails { get; set; }
        public string? LogoUrl { get; set; }
        public bool IsPrefrerred { get; set; }
        public int NetworkTypeID { get; set; }
        public string? CustomerSupportContact { get; set; }
        public string? AcceptedRegions { get; set; }
        public decimal? MaxCoverageAmount { get; set; }
        public bool IsActive { get; set; }


    }
}
