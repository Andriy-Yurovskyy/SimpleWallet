namespace SimpleWallet.Currencies
{
    using SimpleWallet.Currencies.API;
    using SimpleWallet.Currencies.Structs;
    using SimpleWallet.StringConstans;
    using UnityEngine;

    public class Currency : ICurrency
    {

        private CurrencyAttributes attributes;

        private int amount;


        public Currency(CurrencyAttributes details, int value)
        {
            attributes = details;
            amount = value;
        }


        public int Get()
        {
            return amount;
        }


        public void Modify(int value)
        {
            if (amount + value < 0)
            {
                Debug.LogError(string.Format(WalletTexts.IcurrencyKeyBellowZeroError, attributes.Key));
                return;
            }
            int oldAmount = amount;
            amount += value;
            WalletManager.FireChangeCurrencyEvent(new CurrencyEventArgs { type = attributes.Key, Value = amount, OldValue = oldAmount });
        }


        public void Set(int value)
        {
            int oldAmount = amount; 
            amount = value;
            WalletManager.FireChangeCurrencyEvent(new CurrencyEventArgs {type = attributes.Key, Value = amount, OldValue = oldAmount});
        }


        public Sprite GetIcon()
        {
            return attributes.Icon;
        }


        public string GetName()
        {
            return attributes.Name;
        }


    }
}