using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Util
{
    public interface IDateFormater
    {
        /// <summary>
        /// format the date as follow : MonthName/yy/dd (example: 08/03/2018 --> MR/18/03)
        /// </summary>
        /// <param name="date"></param>
        /// <param name="monthsNames">The name of each month (must be 12 element, otherwise an exception is thrown)</param>
        /// <returns></returns>
        string FormateDateToCodeLot(DateTime date, string[] monthsNames);

        /// <summary>
        /// format the date as follow: MMMM dd yyyy (example: 08/03/2018 --> mars 03 2018)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        string FormatDateToNomLot(DateTime date);

    }
}
