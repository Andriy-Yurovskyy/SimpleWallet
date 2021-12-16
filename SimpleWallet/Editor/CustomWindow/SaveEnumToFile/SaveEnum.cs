namespace SimpleWallet.Editor.CustomWindow.SaveEnumTofile
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using SimpleWallet.StringConstans;
    using SimpleWallet.ErrorsHandler;
    using UnityEngine;

    public class SaveEnum
    {

        private List<string> strings;


        public SaveEnum (List<string> objToSave) {
            strings = objToSave;
        }


        public bool CheckForErrors()
        {
            bool errorFlag = false;
            string errorText = null;
            for (int i = 0; i < strings.Count; i++)
            {
                string value = strings[i];

                if (ErrorsHandler.IsEmptyValueError(value, i, out errorText))
                {
                    errorFlag = true;
                    Debug.LogError(errorText);
                }

                if (ErrorsHandler.IsDublicatedError(strings, value, i, out errorText))
                {
                    errorFlag = true;
                    Debug.LogError(errorText);
                }

                if (ErrorsHandler.IsMatchPatternError(value, out errorText))
                {
                    errorFlag = true;
                    Debug.LogError(errorText);
                }
            }

            return errorFlag;
        }


        public void Save()
        {
            string fileContent = String.Format(CurrencyEnumFileDetails.CurrencyEnumFileTemplate, GetEnumString());
            string pathtoSave = String.Format(CurrencyEnumFileDetails.PathToSaveCurrencyEnum, Application.dataPath);
            File.WriteAllText(pathtoSave, fileContent);
        }


        private string GetEnumString()
        {
            return String.Join($",{Environment.NewLine}\t\t", strings.ToArray());
        }


    }
}