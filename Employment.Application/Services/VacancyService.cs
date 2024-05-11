using AutoMapper;
using Employment.Application.DTOs;
using Employment.Application.IServices;
using Employment.Application.ViewModels;
using Employment.Domain.Entities;
using Employment.Domain.IRepository;
using Employment.Infra.Data;
using Employment.Infra.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.Services
{


    public class VacancyService : GenericService, IVacancyService
    {
        protected readonly IVacancyRepository _vacancyRepository;
        public VacancyService(IVacancyRepository vacancyRepository, IMapper mapper) : base(mapper)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<IEnumerable<List<VacancyDTO>>> GetAllVacancies()
        {
            var results = await _vacancyRepository.GetAllVacancies();

            return _mapper.Map<IEnumerable<List<VacancyDTO>>>(results);

        }
        public async Task<bool> AddVacancy(VacancyDTO vacancyDTO)
        {
            return await _vacancyRepository.AddVacancy(_mapper.Map<Vacancy>(vacancyDTO));
        }

        public async Task<bool> EditVacancy(VacancyDTO vacancyDTO)
        {
            if (vacancyDTO != null)
            {
               return  await _vacancyRepository.EditVacancy(_mapper.Map<Vacancy>(vacancyDTO));
            }
            return false;
        }

        public async Task<bool> DeleteVacancy(int vacancyId)
        {
            if (vacancyId != 0)
            {
                return await _vacancyRepository.DeleteVacancy(vacancyId);
            }
            return false;
        }

        public async Task<bool> DeactivateVacancy(int vacancyId)
        {
            if (vacancyId != 0)
            {
                return await _vacancyRepository.DeactivateVacancy(vacancyId);
            }
            return false;
        }

        public async Task<bool> ApplytoVacancy(string userId, int vacancyId)
        {
            if((!string.IsNullOrEmpty(userId)) && vacancyId != 0) 
            {

                VacanciesApplicationDTO dto = new VacanciesApplicationDTO
                {
                    ApplicationDate = DateTime.Now,
                    FkApplicantId = userId,
                    FkVacancyId = vacancyId
                };
                return await _vacancyRepository.ApplytoVacancy(_mapper.Map<VacanciesApplication>(dto));

            }
            return false;
        }

        public Task<IEnumerable<List<UserDto>>> GetAllVacancyApplicants(int vacancyId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PostVacancy(int vacancyId)
        {
            if (vacancyId != 0)
            {
                return await _vacancyRepository.PostVacancy(vacancyId);
            }
            return false;
        }

        private bool ValidateApplication(string userId, int vacancyId)
        {
            //check Existence of same application with same vacancy
            if (CheckApplicationExist(userId, vacancyId))
            {
                //exist
                return false;
            }
            if (CheckApplicationMaxTime(userId))
            {
                //exist app with same day
                return false;
            }
            if(CheckApplicationMaxNumber(vacancyId) == false)
            {
                return false;
            }
            return true;
        }
        private bool CheckApplicationExist(string userId , int vacancyId)
        {
           var results = _vacancyRepository.CheckApplicationExist( userId,  vacancyId);
            if(results != null && results.Count > 0)
            {
                return true;
            }
            return false;
        }

        private bool CheckApplicationMaxTime(string userId)
        {

            var results = _vacancyRepository.CheckApplicationMaxTime(userId);
            if (results != null && results.Count > 0)
            {
                return true;
            }
            return false;
        }


        private bool CheckApplicationMaxNumber(int vacancyId)
        {
            var vacancy = _vacancyRepository.GetVacancyById(vacancyId);
            if(vacancy != null)
            {
                if(vacancy.CurrentNumberOfApplication >= vacancy.MaxNumberOfApplications)
                {
                    return false;
                }
                return true;
            }
            return false;

        }

        public void Dispose()
        {
        }



       
    }
}
