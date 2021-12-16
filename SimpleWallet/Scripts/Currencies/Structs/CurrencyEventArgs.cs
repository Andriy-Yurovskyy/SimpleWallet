namespace SimpleWallet.Currencies.Structs
{
    using SimpleWallet.Currencies.Enums;

    public struct CurrencyEventArgs
    {
        public CurrencyType type;
        public int OldValue;
        public int Value;

    }
}