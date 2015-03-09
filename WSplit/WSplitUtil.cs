using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

class WSplitUtil
{
    public static double timeParse(string timeString)
    {
        double num = 0.0;
        foreach (string str in timeString.Split(new char[] { ':' }))
        {
            double num2;
            if (double.TryParse(str, NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture(""), out num2))
                num = (num * 60.0) + num2;
        }
        return num;
    } 
}

