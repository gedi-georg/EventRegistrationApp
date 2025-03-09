using EventRegistration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegistration.Application.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
