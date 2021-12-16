namespace SimpleWallet.Currencies.API
{
    using UnityEngine;

    public interface ICurrency
    {
        public int Get();

        public void Modify(int amount);

        public void Set(int amount);

        public Sprite GetIcon();

        public string GetName();


    }
}