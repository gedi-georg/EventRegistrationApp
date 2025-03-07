using EventRegistration.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventRegistration.Infra.Interfaces;
using EventRegistration.Domain;
using EventRegistration.Application.Helpers;

namespace EventRegistration.Application.Services
{
    public class CompanyService
    {
        private readonly ICompanyService _companyService;
        private readonly MappingHelper _mappingHelper;

        public CompanyService(ICompanyService companyService, MappingHelper mappingHelper)
        {
            _companyService = companyService;
            _mappingHelper = mappingHelper;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _companyService.GetAllAsync();
            var companyDtos = new List<CompanyDto>();

            foreach (var companyEntity in companies)
            {
                var companyDto = _mappingHelper.GetCompanyDto(companyEntity);
                companyDtos.Add(companyDto);
            }
            return companyDtos;
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(int id)
        {
            var companyEntity = await _companyService.GetByIdAsync(id);
            return companyEntity == null ? null : _mappingHelper.GetCompanyDto(companyEntity);
        }

        public async Task AddCompanyAsync(CompanyDto companyDto)
        {
            var companyEntity = _mappingHelper.CreateCompanyFromDto(companyDto);
            await _companyService.AddAsync(companyEntity);
        }

        public async Task DeleteCompanyAsync(int id)
        {
            var companyEntity = await _companyService.GetByIdAsync(id);
            if (companyEntity == null)
                throw new KeyNotFoundException("Company not found");

            await _companyService.DeleteAsync(companyEntity);
        }
    }
}
