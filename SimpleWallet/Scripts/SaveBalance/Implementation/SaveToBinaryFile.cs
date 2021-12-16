namespace SimpleWallet.SaveBalance.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using SimpleWallet.Currencies.Enums;
    using SimpleWallet.SaveBalance.API;
    using SimpleWallet.StringConstans;
    using UnityEngine;


    public class SaveToBinaryFile: IWalletSaveBalance
    {
        BinaryFormatter binaryFormatter;
        FileStream fileStream;

        public Dictionary<CurrencyType, int> Load(List<CurrencyType> currencyTypes)
        {
            binaryFormatter = new BinaryFormatter();
            string filepath = String.Format(SaveToBinaryFileDetails.PathToSave, Application.persistentDataPath);
            Dictionary<CurrencyType, int> balanceFromFile = new Dictionary<CurrencyType, int>();
            if (File.Exists(filepath))
            {
                fileStream = File.Open(filepath, FileMode.Open);
                balanceFromFile = (Dictionary<CurrencyType, int>)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
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
            binaryFormatter = new BinaryFormatter();
            fileStream = File.Create(String.Format(SaveToBinaryFileDetails.PathToSave, Application.persistentDataPath));
            binaryFormatter.Serialize(fileStream, balance);
            fileStream.Close();
        }


    }
}