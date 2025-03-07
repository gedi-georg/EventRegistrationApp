using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegistration.Application.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidPersonalId(string personalId)
        {
            return personalId.Length == 11 && personalId.All(char.IsDigit);
        }
    }
}
