using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LMG.FAB.Util
{
    public class DateFormater : IDateFormater
    {

        public string FormatDateToNomLot(DateTime date)
        {
            CultureInfo culture = new CultureInfo("fr-FR");
            var result = date.ToString("MMMM dd yyyy", culture);
            result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.ToLower());
            return result;
        }

        public string FormateDateToCodeLot(DateTime date, string[] monthsNames)
        {
            if (date == null)
            {
                throw new Exception("La date de l'office est nulle");
            }
            if (monthsNames == null || monthsNames.Length != 12)
            {
                throw new Exception("Les noms des mois ne sont pas valide, il faut avoir une liste de 12 elements");
            }
            int day = date.Day;
            string year = date.Year.ToString().Substring(2, 2);
            string month = monthsNames?[date.Month - 1];
            return $"{month}/{year}/{day.ToString()}";
        }
    }
}
