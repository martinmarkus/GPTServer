using System.Globalization;

namespace GPTServer.Common.Core.Utils.GeneralUtils.Number;

public static class NumberUtils
{
    public static string SeparateThounsands(this int number)
    {
        NumberFormatInfo formatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        formatInfo.NumberGroupSeparator = " ";
        return number.ToString("#,0", formatInfo);
    }

    public static string SeparateThounsands(this decimal number, bool useDecimals = false)
    {
        NumberFormatInfo formatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        formatInfo.NumberGroupSeparator = " ";
        if (!useDecimals)
        {
            return number.ToString("#,0", formatInfo);
        } 

        string result = number.ToString("#,0.##", formatInfo);
        string separator;

        string decimalPart;
        if (result.Contains('.'))
        {
            decimalPart = result.Split('.')[1];
            separator = ".";
        }
        else if (result.Contains(','))
        {
            decimalPart = result.Split(',')[1];
            separator = ",";
        }
        else
        {
            return $"{result}.00";
        }

        if (decimalPart.Length == 2)
        {
            return result;
        }

        if (decimalPart.Length == 1)
        {
            return $"{result}0";
        }

        return $"{result}{separator}00";
    }
}
