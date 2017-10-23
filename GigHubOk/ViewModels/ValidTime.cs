using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHubOk.ViewModels
{
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime data;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "HH:mm",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out data);

            return (isValid);
        }

    }
}