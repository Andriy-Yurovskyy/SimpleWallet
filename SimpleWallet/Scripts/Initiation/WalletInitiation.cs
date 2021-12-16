namespace SimpleWallet.Initiation
{
    using System.Collections.Generic;
    using SimpleWallet.Currencies;
    using SimpleWallet.Currencies.Enums;
    using SimpleWallet.Currencies.API;
    using SimpleWallet.Currencies.Structs;

    public class WalletInitiation
    {
        private List<CurrencyAttributes> currencies;



        public WalletInitiation(List<CurrencyAttributes> availableCurrencies)
        {
            currencies = availableCurrencies;

        }


        public Dictionary<CurrencyType, ICurrency> InitCurrencies()
        {
            Dictionary<CurrencyType, ICurrency> result = new Dictionary<CurrencyType, ICurrency>();
            foreach (CurrencyAttributes currency in currencies)
            {
                ICurrency currencyWalletObj = new Currency(currency, 0);
                result.Add(currency.Key, currencyWalletObj);
            }
            return result;
        }


    }
}