using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexSolutions.DTOs;
using ApexSolutions.Models;
using ApexSolutions.Interfaces;
using Microsoft.Extensions.Logging;

namespace ApexSolutions.Services
{
    public class TechnicianService : ITechnicianService
    {
        private readonly ITechnicianRepository _technicianRepository;
        private readonly ILogger<TechnicianService> _logger;

        public TechnicianService(ITechnicianRepository technicianRepository, ILogger<TechnicianService> logger)
        {
            _technicianRepository = technicianRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<TechnicianDTO>> GetAllTechniciansAsync()
        {
            try
            {
                var technicians = await _technicianRepository.GetAllAsync();
                return technicians.Select(t => new TechnicianDTO
                {
                    TechnicianID = t.TechnicianID,
                    Name = t.Name,
                    Skills = t.Skills,
                    AvailabilityStatus = t.AvailabilityStatus,
                    AssignedRequestIDs = t.AssignedRequestIDs,
                    ContactNumber = t.ContactNumber
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all technicians.");
                return Enumerable.Empty<TechnicianDTO>();
            }
        }

        public async Task<TechnicianDTO> GetTechnicianByIdAsync(int id)
        {
            try
            {
                var technician = await _technicianRepository.GetByIdAsync(id);
                if (technician == null)
                {
                    _logger.LogWarning($"Technician with ID {id} not found.");
                    return null;
                }

                return new TechnicianDTO
                {
                    TechnicianID = technician.TechnicianID,
                    Name = technician.Name,
                    Skills = technician.Skills,
                    AvailabilityStatus = technician.AvailabilityStatus,
                    AssignedRequestIDs = technician.AssignedRequestIDs,
                    ContactNumber = technician.ContactNumber
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving technician with ID {id}.");
                return null;
            }
        }

        public async Task<TechnicianDTO> CreateTechnicianAsync(TechnicianDTO technicianDto)
        {
            try
            {
                var technician = new Technician
                {
                    Name = technicianDto.Name,
                    Skills = technicianDto.Skills,
                    AvailabilityStatus = technicianDto.AvailabilityStatus,
                    AssignedRequestIDs = technicianDto.AssignedRequestIDs,
                    ContactNumber = technicianDto.ContactNumber
                };

                var createdTechnician = await _technicianRepository.AddAsync(technician);
                return new TechnicianDTO
                {
                    TechnicianID = createdTechnician.TechnicianID,
                    Name = createdTechnician.Name,
                    Skills = createdTechnician.Skills,
                    AvailabilityStatus = createdTechnician.AvailabilityStatus,
                    AssignedRequestIDs = createdTechnician.AssignedRequestIDs,
                    ContactNumber = createdTechnician.ContactNumber
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new technician.");
                return null;
            }
        }

        public async Task<TechnicianDTO> UpdateTechnicianAsync(int id, TechnicianDTO technicianDto)
        {
            try
            {
                var technician = await _technicianRepository.GetByIdAsync(id);
                if (technician == null)
                {
                    _logger.LogWarning($"Technician with ID {id} not found.");
                    return null;
                }

                // Update properties
                technician.Name = technicianDto.Name;
                technician.Skills = technicianDto.Skills;
                technician.AvailabilityStatus = technicianDto.AvailabilityStatus;
                technician.AssignedRequestIDs = technicianDto.AssignedRequestIDs;
                technician.ContactNumber = technicianDto.ContactNumber;

                await _technicianRepository.UpdateAsync(technician);

                return technicianDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating technician with ID {id}.");
                return null;
            }
        }

        public async Task<bool> DeleteTechnicianAsync(int id)
        {
            try
            {
                var technician = await _technicianRepository.GetByIdAsync(id);
                if (technician == null)
                {
                    _logger.LogWarning($"Technician with ID {id} not found.");
                    return false;
                }

                await _technicianRepository.DeleteAsync(technician.TechnicianID);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting technician with ID {id}.");
                return false;
            }
        }
    }
}
