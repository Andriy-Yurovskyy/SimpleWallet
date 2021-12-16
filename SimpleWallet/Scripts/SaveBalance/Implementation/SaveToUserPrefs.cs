namespace SimpleWallet.SaveBalance.Implementation
{
    using System.Collections.Generic;
    using SimpleWallet.Currencies.Enums;
    using SimpleWallet.SaveBalance.API;
    using UnityEngine;

    public class SaveToUserPrefs : IWalletSaveBalance
    {
        public Dictionary<CurrencyType, int> Load(List<CurrencyType> currencyTypes)
        {
            Dictionary<CurrencyType, int> result = new Dictionary<CurrencyType, int>();
            foreach (CurrencyType type in currencyTypes)
            {
                result.Add(type, PlayerPrefs.GetInt(type.ToString(), 0));
            }
            return result;
        }


        public void Save(Dictionary<CurrencyType, int> balance)
        {
            foreach (var keyValuePair in balance)
            {
                PlayerPrefs.SetInt(keyValuePair.Key.ToString(), keyValuePair.Value);
            }
        }


    }
}