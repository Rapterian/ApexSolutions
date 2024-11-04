using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexSolutions.DTOs;
using ApexSolutions.Models;
using ApexSolutions.Interfaces;

namespace ApexSolutions.Services
{
    public class TechnicianService : ITechnicianService
    {
        private readonly ITechnicianRepository _technicianRepository;

        public TechnicianService(ITechnicianRepository technicianRepository)
        {
            _technicianRepository = technicianRepository;
        }

        public async Task<IEnumerable<TechnicianDTO>> GetAllTechniciansAsync()
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

        public async Task<TechnicianDTO> GetTechnicianByIdAsync(int id)
        {
            var technician = await _technicianRepository.GetByIdAsync(id);
            if (technician == null)
            {
                return null; // Return null if not found
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

        public async Task<TechnicianDTO> CreateTechnicianAsync(TechnicianDTO technicianDto)
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

        public async Task<TechnicianDTO> UpdateTechnicianAsync(int id, TechnicianDTO technicianDto)
        {
            var technician = await _technicianRepository.GetByIdAsync(id);
            if (technician == null)
            {
                return null; // Return null if not found
            }

            // Update properties
            technician.Name = technicianDto.Name;
            technician.Skills = technicianDto.Skills;
            technician.AvailabilityStatus = technicianDto.AvailabilityStatus;
            technician.AssignedRequestIDs = technicianDto.AssignedRequestIDs;
            technician.ContactNumber = technicianDto.ContactNumber;

            await _technicianRepository.UpdateAsync(technician); // Assuming you have this method in your repository

            return technicianDto; // Return the updated DTO
        }

        public async Task<bool> DeleteTechnicianAsync(int id)
        {
            var technician = await _technicianRepository.GetByIdAsync(id);
            if (technician == null)
            {
                return false; // Return false if not found
            }

            await _technicianRepository.DeleteAsync(technician.TechnicianID); // Assuming you have this method in your repository
            return true; // Return true if deletion was successful
        }
    }
}