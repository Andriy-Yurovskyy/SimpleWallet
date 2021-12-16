namespace SimpleWallet.ErrorsHandler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using SimpleWallet.StringConstans;
    using SimpleWallet.Helpers;

    public class ErrorsHandler
    {

        public static bool IsMatchPatternError(string value, out string error)
        {
            error = null;
            if (Regex.IsMatch(value, @"^[a-zA-Z]+$"))
            {
                return false;
            }
            error = WalletTexts.CurrencyKeyPatternMatchError;
            return true;
        }


        public static bool IsEmptyValueError(string value, int index, out string error)
        {
            error = null;
            if (!WalletHelper.IsEmptyString(value))
            {
                return false;
            }
            error = String.Format(WalletTexts.CurrencyKeyEmptyKeyErrorDialogText, index);
            return true;
        }


        public static bool IsDublicatedError(List<string> list, string value, int index, out string error)
        {
            error = null;

            List<int> duplicates = Enumerable.Range(0, list.Count)
             .Where(i => list[i] == value)
             .ToList();
            duplicates.Remove(index);

            if (duplicates.Count == 0)
            {
                return false;
            }
            error = String.Format(WalletTexts.CurrencyKeyDublicatedError, String.Join(",", duplicates.ToArray()));
            return true;
        }


    }
}
    
