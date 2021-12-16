namespace SimpleWallet.Currencies.Structs
{
    using System;
    using SimpleWallet.Currencies.Enums;
    using UnityEngine;

    [Serializable]
    public struct CurrencyAttributes
    {
        public string Name;
        public CurrencyType Key;
        public Sprite Icon;
    }
}

