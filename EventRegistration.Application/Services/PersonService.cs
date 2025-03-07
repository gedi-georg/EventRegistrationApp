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
    public class PersonService
    {
        private readonly IPersonService _personService;
        private readonly MappingHelper _mappingHelper;

        public PersonService(IPersonService personService, MappingHelper mappingHelper)
        {
            _personService = personService;
            _mappingHelper = mappingHelper;
        }

        public async Task<IEnumerable<PersonDto>> GetAllPersonsAsync()
        {
            var persons = await _personService.GetAllAsync();
            var personDtos = new List<PersonDto>();

            foreach (var personEntity in persons)
            {
                personDtos.Add(_mappingHelper.GetPersonDto(personEntity));
            }
            return personDtos;
        }

        public async Task<PersonDto> GetPersonByIdAsync(int id)
        {
            var personEntity = await _personService.GetByIdAsync(id);
            return personEntity == null ? null : _mappingHelper.GetPersonDto(personEntity);
        }

        public async Task AddPersonAsync(PersonDto personDto)
        {
            var personEntity = _mappingHelper.CreatePersonFromDto(personDto);
            await _personService.AddAsync(personEntity);
        }

        public async Task DeletePersonAsync(int id)
        {
            var personEntity = await _personService.GetByIdAsync(id);
            if (personEntity == null) throw new KeyNotFoundException("Person not found");

            await _personService.DeleteAsync(personEntity);
        }
    }
}
