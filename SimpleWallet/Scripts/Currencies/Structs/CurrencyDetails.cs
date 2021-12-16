namespace SimpleWallet.Currencies.Structs
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct CurrencyDetails
    {
        public string Name;
        public int Value;
        public Sprite Icon;
    }
}