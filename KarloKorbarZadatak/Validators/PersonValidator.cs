using System.Text.RegularExpressions;

namespace KarloKorbarZadatak.Validators
{
    public class PersonValidator : IValidator<Person>
    {
        public bool IsValid(Person value)
        {
            bool isValid = 
                value.Ime != null &&
                value.Prezime != null &&
                value.PostanskiBr != null &&
                Regex.IsMatch(value.PostanskiBr, "^[0-9]*$") &&
                value.Grad != null &&
                value.Telefon != null;

            return isValid;
        }
    }
}
