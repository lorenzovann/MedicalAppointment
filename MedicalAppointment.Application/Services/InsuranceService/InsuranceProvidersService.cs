

using MedicalAppointment.Application.Contracts.InsuranceContracts;
using MedicalAppointment.Application.Dto.DtosInsurance.InsuranceProvidersDtos;
using MedicalAppointment.Application.Responses.InsuranceResponses;
using MedicalAppointment.Persistance.Interfaces.Insurance;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Application.Services.InsuranceService
{
    public class InsuranceProvidersService : IInsuranceProvidersService
    {
        private readonly IInsuranceProvidersRepository _insuranceProvidersRepository;
        private readonly ILogger<InsuranceProvidersService> _logger;

        public InsuranceProvidersService(IInsuranceProvidersRepository insuranceProvidersRepository, 
                                         ILogger<InsuranceProvidersService> logger)
        {
            _insuranceProvidersRepository = insuranceProvidersRepository;
            _logger = logger;
        }

        public InsuranceProvidersService() { }

        public Task<InsuranceProvidersResponse> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<InsuranceProvidersResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<InsuranceProvidersResponse> SaveAsync(InsuranceProvidersUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<InsuranceProvidersResponse> UpdateAsync(InsuranceProvidersUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
