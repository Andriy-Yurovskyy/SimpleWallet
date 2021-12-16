namespace SimpleWallet.SaveBalance.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using SimpleWallet.Currencies.Enums;
    using SimpleWallet.SaveBalance.API;
    using SimpleWallet.StringConstans;
    using UnityEngine;

    public class SaveToTxtFile : IWalletSaveBalance
    {
        public Dictionary<CurrencyType, int> Load(List<CurrencyType> currencyTypes)
        {
            Dictionary<CurrencyType, int> balanceFromFile = new Dictionary<CurrencyType, int>();
            string filepath = String.Format(SaveToTxtFileDetails.PathToSave, Application.persistentDataPath);
            if (File.Exists(filepath))
            {
                string content = File.ReadAllText(filepath);
                string[] splittedContent = content.Split(',');
                for (int i = 0; i < splittedContent.Length - 1; i = i + 2)
                {
                    balanceFromFile.Add((CurrencyType)Enum.Parse(typeof(CurrencyType), splittedContent[i]), Int32.Parse(splittedContent[i + 1]));
                }
            }

            Dictionary<CurrencyType, int> result = new Dictionary<CurrencyType, int>();
            foreach (CurrencyType type in currencyTypes)
            {
                result.Add(type, (balanceFromFile.ContainsKey(type)) ? balanceFromFile[type] : 0);
            }

            return result;
        }


        public void Save(Dictionary<CurrencyType, int> balance)
        {
            string content = null;
            foreach (var kvp in balance)
            {
                content += $"{kvp.Key},{kvp.Value},";
            }
            File.WriteAllText(String.Format(SaveToTxtFileDetails.PathToSave, Application.persistentDataPath), content);
        }


    }
}