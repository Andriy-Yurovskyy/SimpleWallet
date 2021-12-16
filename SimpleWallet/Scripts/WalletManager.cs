namespace SimpleWallet
{
    using System.Collections.Generic;
    using System.Linq;
    using SimpleWallet.Currencies.Structs;
    using SimpleWallet.Currencies.Enums;
    using SimpleWallet.SaveBalance.Enums;
    using UnityEngine;
    using SimpleWallet.Initiation;
    using SimpleWallet.Currencies.API;
    using System;
    using SimpleWallet.StringConstans;
    using SimpleWallet.SaveBalance.API;
    using SimpleWallet.SaveBalance.Implementation;

    public class WalletManager : MonoBehaviour
    {
        public static event EventHandler<CurrencyEventArgs> OnCurrencyValueChanged;

        [SerializeField]
        private CurrencyAttributes[] availableCurrencies;
        private static CurrencyAttributes[] AvailableCurrencies;

        [SerializeField]
        private SaveSettingsType saveLoadSettingsType;
        private static SaveSettingsType SaveLoadSettingsType;

        private static bool isInitiated = false;

        private static Dictionary<CurrencyType, ICurrency> currencies;


        private void Awake()
        {
            if (isInitiated)
            {
                return;
            }
            WalletInitiation initWallet = new WalletInitiation(availableCurrencies.ToList());
            currencies = initWallet.InitCurrencies();
            isInitiated = true;
            SaveLoadSettingsType = saveLoadSettingsType;
            AvailableCurrencies = availableCurrencies;
        }


        public static void ModifyCurrencyValue(CurrencyType type, int amount)
        {
            ICurrency currency;

            if (!IsInitiated())
            {
                return;
            }
            if (!GetCurrencyByType(type, out currency))
            {
                return;
            }

            currency.Modify(amount);
        }


        public static void SetCurrencyValue(CurrencyType type, int amount)
        {
            ICurrency currency;

            if (!IsInitiated())
            {
                return;
            }

            if (!GetCurrencyByType(type, out currency))
            {
                return;
            }

            currency.Set(amount);
        }


        public static int GetCurrencyValue(CurrencyType type)
        {
            ICurrency currency;
            int result = 0;

            if (!IsInitiated())
            {
                return result;
            }
            if (!GetCurrencyByType(type, out currency))
            {
                return result;
            }

            currency.Get();
            return result;
        }


        public static CurrencyDetails GetCurrencyDetails(CurrencyType type)
        {
            CurrencyDetails result = new CurrencyDetails();
            ICurrency currency;
            if (!IsInitiated())
            {
                return result;
            }

            if (!GetCurrencyByType(type, out currency))
            {
                return result;
            }

            result.Value = GetCurrencyValue(type);
            result.Icon = currency.GetIcon();
            result.Name = currency.GetName();

            return result;
        }


        public static Dictionary<CurrencyType, int> GetBalance()
        {
            Dictionary<CurrencyType, int> result = new Dictionary<CurrencyType, int>();
            if (!IsInitiated())
            {
                return result;
            }

            foreach (var keyValuePair in currencies)
            {
                result.Add(keyValuePair.Key, keyValuePair.Value.Get());
            }

            return result;
        }


        public static void NullifyWalletBalance()
        {
            if (!IsInitiated())
            {
                return;
            }

            foreach (var keyValuePair in currencies)
            {
                keyValuePair.Value.Set(0);
            }
        }


        public static void FireChangeCurrencyEvent(CurrencyEventArgs args)
        {
            if (!IsInitiated())
            {
                return;
            }

            OnCurrencyValueChanged?.Invoke(null, args);
        }


        public static void SaveBalance()
        {
            IWalletSaveBalance balanceObj = GetSaveBalanceObject();
            balanceObj.Save(GetBalance());
        }


        public static void LoadBalance()
        {
            IWalletSaveBalance balanceObj = GetSaveBalanceObject();
            ICurrency currency;
            foreach (var keyValuePair in balanceObj.Load(GetCurrencyTypes()))
            {
                if (!GetCurrencyByType(keyValuePair.Key, out currency))
                {
                    return;
                }
                currency.Set(keyValuePair.Value);
            }
        }


        private static List<CurrencyType> GetCurrencyTypes()
        {
            List<CurrencyType> result = new List<CurrencyType>();
            for (int i = 0; i < AvailableCurrencies.Length; i++)
            {
                result.Add(AvailableCurrencies[i].Key);
            }
            return result;

        }


        private static bool GetCurrencyByType(CurrencyType type, out ICurrency currency)
        {
            currency = null;
            if (!IsInitiated())
            {
                return false;
            }
            if (!currencies.ContainsKey(type))
            {
                Debug.LogError(string.Format(WalletTexts.WalletManagerCurrencyKeyWasNotFound, type));
                return false;
            }

            currency = currencies[type];
            return true;
        }


        public static bool IsInitiated()
        {
            if (!isInitiated)
            {
                Debug.LogError(WalletTexts.WalletManagerIsNotInitiatedError);
            }
            return isInitiated;
        }


        private static IWalletSaveBalance GetSaveBalanceObject()
        {
            switch (SaveLoadSettingsType)
            {
                case SaveSettingsType.BinaryFile:
                    return new SaveToBinaryFile();

                case SaveSettingsType.TextFile:
                    return new SaveToTxtFile();

                default:
                    return new SaveToUserPrefs();
            }
        }


    }
}