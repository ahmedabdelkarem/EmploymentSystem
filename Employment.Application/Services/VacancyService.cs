using AutoMapper;
using Employment.Application.DTOs;
using Employment.Application.IServices;
using Employment.Application.ViewModels;
using Employment.Domain.IRepository;
using Employment.Infra.Data;
using Employment.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.Services
{
  

    public class VacancyService : GenericService , IVacancyService 
    {
        protected readonly  IVacancyRepository _vacancyRepository;
        public VacancyService(IVacancyRepository vacancyRepository, IMapper mapper ) : base (mapper)
        {
            _vacancyRepository = vacancyRepository;
        }
        public Task<IEnumerable<bool>> AddVacancy(VacancyDTO vacancyDTO)
        {
           
            throw new NotImplementedException();
        }

        public Task<IEnumerable<bool>> ApplytoVacancy(int userId, int vacancyId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<bool>> DeactivateVacancy(int vacancyId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<bool>> DeleteVacancy(int vacancyId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<IEnumerable<bool>> EditVacancy(VacancyDTO vacancyDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<List<VacancyDTO>>> GetAllVacancies()
        {
            var results = await _vacancyRepository.GetAllVacancies();
         
           return _mapper.Map<IEnumerable<List<VacancyDTO>>>(results);

        }

        public Task<IEnumerable<List<UserDto>>> GetAllVacancyApplicants(int vacancyId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<bool>> PostVacancy(int vacancyId)
        {
            throw new NotImplementedException();
        }
    }
}
