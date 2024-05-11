using Employment.Domain.Entities;
using Employment.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Infra.Data.Repository
{
    public class VacancyRepository : GenericRepository<Vacancy>, IVacancyRepository
    {
        public VacancyRepository(EmploymentContext DBContext) : base(DBContext)
        {
        }

        public async Task<IEnumerable<List<Vacancy>>> GetAllVacancies()
        {
            var result = _dbContext.Vacancies.Where(vac => vac.IsActive == true
                                         && vac.IsExpired == false
                                         && vac.IsPosted == true);

            return (IEnumerable<List<Vacancy>>)result;
        }

        public async Task<bool> AddVacancy(Vacancy vacancy)
        {
            return Insert(_dbContext, vacancy);

        }

        public async Task<bool> EditVacancy(Vacancy vacancy)
        {
            return Update(_dbContext, vacancy);
        }

        public async Task<bool> DeleteVacancy(int vacancyId)
        {
            return Delete(_dbContext, vacancyId);
        }

        public async Task<bool> DeactivateVacancy(int vacancyId)
        {
            var vacancy = GetById(_dbContext, vacancyId);
            if (vacancy != null)
            {
                vacancy.IsActive = false;
                return Update(_dbContext, vacancy);
            }
            return false;
        }

        public async Task<bool> ApplytoVacancy(VacanciesApplication vacanciesApplication)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PostVacancy(int vacancyId)
        {
            var vacancy = GetById(_dbContext, vacancyId);
            if (vacancy != null)
            {
                vacancy.IsPosted = true;
                return Update(_dbContext, vacancy);
            }
            return false;
        }
        public List<VacanciesApplication> CheckApplicationExist(string userId, int vacancyId)
        {
            List<VacanciesApplication> vacancyApp = new List<VacanciesApplication>();

            vacancyApp = _dbContext.VacanciesApplications.Where(a => a.FkApplicantId.Trim() == userId.Trim()
            && a.FkVacancyId == vacancyId).ToList();

            return vacancyApp;

        }
        public List<VacanciesApplication> CheckApplicationMaxTime(string userId)
        {
            List<VacanciesApplication> vacancyApp = new List<VacanciesApplication>();

            vacancyApp = _dbContext.VacanciesApplications.Where(a => a.FkApplicantId.Trim() == userId.Trim()
            && a.ApplicationDate.Value.Date == DateTime.Now.Date).ToList();

            return vacancyApp;
        }

        public Vacancy CheckApplicationMaxNumber(int vacancyId)
        {
            var vacancy = GetById(_dbContext, vacancyId);
            return vacancy;

        }

        public Vacancy GetVacancyById(int vacancyId)
        {
            var vacancy = GetById(_dbContext, vacancyId);
            return vacancy;



        }

    }
}


