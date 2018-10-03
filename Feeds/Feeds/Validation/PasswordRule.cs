using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Feeds
{
    class PasswordRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            // should contain at least 8 characters, 1 numeric, 1 lowercase, 1 uppercase, 1 special character 
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$");
            Match match = regex.Match(str);

            return match.Success;
        }
    }
}
