using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegistration.Domain.Models
{
    public class Event : Entity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string? AdditionalInfo { get; set; }

        public ICollection<Participant> Participants { get; set; }

        public Event()
        {
            Participants = new List<Participant>();
        }
    }
}
