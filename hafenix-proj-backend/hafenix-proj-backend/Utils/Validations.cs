using System.Text.RegularExpressions;

namespace hafenix_proj_backend.Utils
{
    public class Validations
    {
        public static bool ValidateAccountNumber(string input){

            if(String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            var regex = new Regex(@"^\d{1,10}$");

            return regex.IsMatch(input);
        }

        public static bool ValidateAmount(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            var regex = new Regex(@"^\d{1,10}$");

            return regex.IsMatch(input);
        }

        public static bool ValidateUserId(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            var regex = new Regex(@"^\d{9}$");

            return regex.IsMatch(input);
        }

    }
}
