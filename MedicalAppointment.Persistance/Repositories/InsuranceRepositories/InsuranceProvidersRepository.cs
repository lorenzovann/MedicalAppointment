
using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Domain.Entities.Insurance;
using MedicalAppointment.Domain.Repositories;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace MedicalAppointment.Persistance.Repositories.InsuranceRepositories
{
    public class InsuranceProvidersRepository : BaseRepository<InsuranceProviders>, IInsuranceProvidersRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<InsuranceProvidersRepository> _logger;

        public InsuranceProvidersRepository(MedicalAppointmentContext context,
               ILogger<InsuranceProvidersRepository> logger) : base(context)
        {

            _medicalAppointmentContext = context;
            _logger = logger;
        }




        public async override Task<OperationResult> Save(InsuranceProviders entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity.InsuranceProviderID == 0 || string.IsNullOrEmpty(entity.Address) || string.IsNullOrEmpty(entity.ContactNumber))
            {
                operationResult.Success = false;
                operationResult.Message = "InsuranceProviderID Address y ContactNumber  son obligatorios.";
                return operationResult;
            }


            if (entity.NetworkTypeID <= 0 || entity.InsuranceProviderID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "NetworkTypeID y InsuranceProviderID debe ser positivo.";
                return operationResult;
            }



            try
            {

                await base.Save(entity);
                operationResult.Data = entity;
                operationResult.Success = true;
                operationResult.Message = "InsuranceProviders guardado exitosamente.";


            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Error {ex.Message} tratando de guardar InsuranceProviders.";
                _logger.LogError(operationResult.Message, ex.ToString());

            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(InsuranceProviders entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity.InsuranceProviderID == 0 || string.IsNullOrEmpty(entity.Address) || string.IsNullOrEmpty(entity.ContactNumber))
            {
                operationResult.Success = false;
                operationResult.Message = "InsuranceProviderID , Address y ContactNumber son obligatorios.";
                return operationResult;
            }


            if (entity.NetworkTypeID <= 0 || entity.InsuranceProviderID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "NetworkTypeID y InsuranceProviderID debe ser positivo.";
                return operationResult;
            }


            try
            {
                InsuranceProviders? insuranceProvidersToUpdate = await _medicalAppointmentContext.InsuranceProviders.FindAsync(entity);

                if (insuranceProvidersToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se puede actualizar el registro.";
                    return operationResult;

                }

                insuranceProvidersToUpdate.InsuranceProviderID = entity.InsuranceProviderID;
                insuranceProvidersToUpdate.Name = entity.Name;
                insuranceProvidersToUpdate.ContactNumber = entity.ContactNumber;
                insuranceProvidersToUpdate.Email = entity.Email;
                insuranceProvidersToUpdate.Website = entity.Website;
                insuranceProvidersToUpdate.Address = entity.Address;
                insuranceProvidersToUpdate.City = entity.City;
                insuranceProvidersToUpdate.State = entity.State;
                insuranceProvidersToUpdate.Country = entity.Country;
                insuranceProvidersToUpdate.ZipCode = entity.ZipCode;
                insuranceProvidersToUpdate.CoverageDetails = entity.CoverageDetails;
                insuranceProvidersToUpdate.LogoUrl = entity.LogoUrl;
                insuranceProvidersToUpdate.IsPreferred = entity.IsPreferred;
                insuranceProvidersToUpdate.NetworkTypeID = entity.NetworkTypeID;
                insuranceProvidersToUpdate.CustomerSupportContact = entity.CustomerSupportContact;
                insuranceProvidersToUpdate.AcceptedRegions = entity.AcceptedRegions;
                insuranceProvidersToUpdate.MaxCoverageAmount = entity.MaxCoverageAmount;
                insuranceProvidersToUpdate.IsActive = entity.IsActive;

                operationResult = await base.Update(insuranceProvidersToUpdate);
                operationResult.Data = insuranceProvidersToUpdate;
                operationResult.Message = "InsuranceProviders actualizado exitosamente.";


            }

            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error actualizando InsuranceProviders.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var insuranceProviders = await _medicalAppointmentContext.InsuranceProviders.ToListAsync();

                operationResult.Data = insuranceProviders;
                operationResult.Success = true;
                operationResult.Message = " InsuranceProviders recuperadas con éxito.";
            } 
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Error {ex.Message} tratando de obtener Appointments.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }



        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new OperationResult();

            if (id <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El id no puede ser menor a cero.";
                return operationResult;
            }

            try
            {
                var insurance = await (from ip in _medicalAppointmentContext.InsuranceProviders
                                       where ip.InsuranceProviderID == id && ip.IsActive == true
                                       select new
                                       {
                                           ip.InsuranceProviderID,
                                           ip.Name,
                                           ip.ContactNumber,
                                           ip.Email,
                                           ip.Website,
                                           ip.Address,
                                           ip.City,
                                           ip.State,
                                           ip.Country,
                                           ip.ZipCode,
                                           ip.CoverageDetails,
                                           ip.LogoUrl,
                                           ip.IsPreferred,
                                           ip.NetworkTypeID,
                                           ip.CustomerSupportContact,
                                           ip.AcceptedRegions,
                                           ip.MaxCoverageAmount,
                                           ip.CreatedAt,
                                           ip.UpdatedAt,
                                           ip.IsActive,


                                       }).FirstOrDefaultAsync();
                if (insurance == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "InsuranceProviders no encontrado.";
                    return operationResult;

                }


            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Ocurrio un error {ex.Message}.";
                _logger.LogError(operationResult.Message, ex.ToString());

            }
            return operationResult;

        }



    }
}

