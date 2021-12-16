namespace SimpleWallet.ExampleScene
{
    using SimpleWallet;
    using SimpleWallet.Currencies.Enums;
    using SimpleWallet.Currencies.Structs;
    using UnityEngine;
    using UnityEngine.UI;

    public class WalletCurrencyView : MonoBehaviour
    {
        [SerializeField]
        private CurrencyType currencyType;

        [SerializeField]
        private Text currencyName;

        [SerializeField]
        private Text currencyValue;

        [SerializeField]
        private Image currencyIcon;


        private void Start()
        {
            if (!WalletManager.IsInitiated())
            {
                return;
            }

            CurrencyDetails details = WalletManager.GetCurrencyDetails(currencyType);
            currencyName.text = details.Name;
            currencyValue.text = details.Value.ToString();
            currencyIcon.sprite = details.Icon;
        }


        private void OnEnable()
        {
            WalletManager.OnCurrencyValueChanged += OnCurrencyValueChangedHandler;
        }


        private void OnDisable()
        {
            WalletManager.OnCurrencyValueChanged -= OnCurrencyValueChangedHandler;
        }


        private void OnCurrencyValueChangedHandler(object sender, CurrencyEventArgs e)
        {
            if (currencyType != e.type)
            {
                return;
            }

            currencyValue.text = e.Value.ToString();
        }


    }
}