
using System.Text.RegularExpressions;

namespace EventRegistration.Application.Helpers;

public static class ValidationHelper
{
    public static bool IsValidPersonalId(string personalId)
    {
        //return personalId.Length == 11 && personalId.All(char.IsDigit);
        return Regex.IsMatch(personalId, @"^\d{11}$");
    }
}