namespace SimpleWallet.ExampleScene
{
    using SimpleWallet;
    using SimpleWallet.Currencies.Enums;
    using UnityEngine;
    using UnityEngine.UI;

    public class ButtonsActions : MonoBehaviour
    {
        [SerializeField]
        private Button add10Coins;

        [SerializeField]
        private Button substract10Coins;

        [SerializeField]
        private Button setCoinsTo100;

        [SerializeField]
        private Button add10Gems;

        [SerializeField]
        private Button substract10Gems;

        [SerializeField]
        private Button setGemsTo100;

        [SerializeField]
        private Button getWalletBalance;

        [SerializeField]
        private Button setCurrenciesToZero;

        [SerializeField]
        private Button saveWalletBalance;

        [SerializeField]
        private Button loadWalletBalance;


        private void Start()
        {
            if (!WalletManager.IsInitiated())
            {
                return;
            }


            add10Coins.onClick.AddListener(() =>
            {
                WalletManager.ModifyCurrencyValue(CurrencyType.Coins, 10);
            });

            add10Gems.onClick.AddListener(() =>
            {
                WalletManager.ModifyCurrencyValue(CurrencyType.Gems, 10);
            });

            substract10Coins.onClick.AddListener(() =>
            {
                WalletManager.ModifyCurrencyValue(CurrencyType.Coins, -10);
            });

            substract10Gems.onClick.AddListener(() =>
            {
                WalletManager.ModifyCurrencyValue(CurrencyType.Gems, -10);
            });

            setCoinsTo100.onClick.AddListener(() =>
            {
                WalletManager.SetCurrencyValue(CurrencyType.Coins, 100);
            });

            setGemsTo100.onClick.AddListener(() =>
            {
                WalletManager.SetCurrencyValue(CurrencyType.Gems, 100);
            });

            getWalletBalance.onClick.AddListener(() =>
            {
                string balance = "Wallet Balance. ";
                foreach (var keyValuePair in WalletManager.GetBalance())
                {

                    balance += $"{keyValuePair.Key}:{keyValuePair.Value}. ";

                }
                Debug.Log(balance);
            });

            setCurrenciesToZero.onClick.AddListener(() =>
            {
                WalletManager.NullifyWalletBalance();
            });

            saveWalletBalance.onClick.AddListener(() =>
            {
                WalletManager.SaveBalance();
            });

            loadWalletBalance.onClick.AddListener(() =>
            {
                WalletManager.LoadBalance();
            });

        }


    }
}