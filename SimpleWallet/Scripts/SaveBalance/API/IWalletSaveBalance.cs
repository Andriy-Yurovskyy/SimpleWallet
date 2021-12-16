namespace SimpleWallet.SaveBalance.API
{
    using System.Collections.Generic;
    using SimpleWallet.Currencies.Enums;
    public interface IWalletSaveBalance {



        public void Save(Dictionary<CurrencyType, int> balance);

        public Dictionary<CurrencyType, int> Load(List<CurrencyType> currencyTypes);

    }
}