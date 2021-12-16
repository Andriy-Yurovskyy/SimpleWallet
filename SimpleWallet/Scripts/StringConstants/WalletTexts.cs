namespace SimpleWallet.StringConstans
{
    public static class WalletTexts
    {
        public const string PackageEditorWindowPath = "Window/SimpleWallet/Settings";

        public const string PackageEditorWindowName = "SimpleWallet Settings";

        public const string CurrenciesKeysInformationLableName = "Currencies Keys Help Information";

        public const string CurrencyTypesExplanation = "Every time you save SimpleWallet settings the enum CurrencyType is generated from the keys values. CurrencyType enum provides convenient access to the SimpleWallet currencies from Inspector and Scripts. Currencies Keys should be unique and match [a-zA-Z] pattern with capitalized first char. \n\nExamples: \nCurrencyKey - is valid. \n1currencyKey, currencyKey, currencyKey1 - are invalid. \nKeys CurrencyKey and currencyKey will cause 'Duplicated Currency Key' error.";

        public const string CurrenciesKeysSetupLableName = "Currencies Keys:";
        public const string CurrencyKeyLable = "Key{0}";

        public const string RemoveCurrencyKeyButtonName = "Remove";
        public const string AddNewcurrencyNameButtonKey = "Add a New Currency Key";

        public const string RemoveCurrencyKeyConfirmDialogTitle = "Warning!";
        public const string RemoveCurrencyKeyConfirmDialogText = "Removing the Currency Key that you are using in the project may cause errors in the Inspector and Scripts";
        public const string RemoveCurrencyKeyConfirmDialogOkButtonText = "Remove";
        public const string RemoveCurrencyKeyConfirmDialogCancelButtonText = "Cancel";

        public const string CurrencyKeyEmptyKeyErrorDialogTitle = "Error";
        public const string CurrencyKeyEmptyKeyErrorDialogText = "Can't add a new Currency Key because Key{0} is empty.";
        public const string CurrencyKeyEmptyKeyErrorDialogOkButtonText = "Ok";

        public const string CurrencyKeyPatternMatchError = "You should use only[a - zA - Z] pattern for Currency Key Value\n\nExamples: \nCurrencyKey - is valid. \n1currencyKey, currencyKey, currencyKey1 - are invalid.";
        public const string CurrencyKeyDublicatedError = "Duplicated Currency Key with Key(s): {0}.\nPlease use unique value for each Currency Key.";
        public const string CurrencyKeyEmptyError = "Currency Key{0} is empty. Please fill correct value.";

        public const string SaveWalletSettingsButtonName = "Save Wallet settings";
        public const string DiscardWalletSettingsButtonName = "Discard Changes";
        public const string SaveWalletSettingsSuccessMessage = "Wallet settings were successfully saved.";

        public const string WalletManagerInspectorKeysNotSetupErrorText = "To use the SimpleWallet functionality, first you need to fill in the SimpleWallet Settings ({0})";
        public const string WalletManagerInspectorOpenSettingsButton = "Settings";
        public const string WalletManagerInspectorCurrenciesLabel = "Currencies";
        public const string WalletManagerInspectorCurrencyKeyFoldoutLabel = "Key{0}({1})";
        public const string WalletManagerInspectorCurrencyNameLabel = "Displayed Name(Optional)";
        public const string WalletManagerInspectorCurrencyKeyLabel = "Key";
        public const string WalletManagerInspectorCurrencyIconLabel = "Icon(Optional)";
        public const string WalletManagerInspectorCurrencyRemoveCurrencyButton = "Remove Key{0}({1}) From Currencies";
        public const string WalletManagerInspectorAddCurrencyButton = "Add a New Currency";
        public const string WalletManagerInspectorSaveBalanceLabel = "Save Balance to:";
        public const string WalletManagerInspectorAddCurrencyErrorErrorsInEditorText = "Can't add a new Currency because there are errors in Wallet Manager. Please correct errors and try again";
        public const string WalletManagerInspectorAddCurrencyErrorLimitText = "Can't add a new Currency because all currencies keys are assigned. Please add a new Currency Key in SimpleWallet settings.";
        public const string WalletManagerInspectorSaveChangesButtonText = "Save Wallet Manager";
        public const string WalletManagerInspectorDiscardChangesButtonText = "Discard Changes";
        public const string WalletManagerInspectorUnsavedChangesText = "Wallet manager detected unsaved changes that will be lost until you press 'Save SimpleWallet Currencies' button.";
        public const string WalletManagerInspectorSaveChangesErrorErrorsInEditorText = "Can't save Currencies because there are errors in Wallet Manager. Please correct errors and try again";
        public const string WalletManagerInspectorNoChangesToSaveText = "There are no changes that should be saved in Wallet Manager";
        public const string WalletManagerInspectorNoChangesToDiscardText = "There are no changes to discard in Wallet Manager";
        public const string WalletManagerInspectorChangesSuccessfullySavedText = "Wallet Manager was successfully saved.";
        public const string WalletManagerInspectorIsNotOnlyOneManagerErrorText = "More then one component of Wallet Manager were found on scene. There can be only one Wallet Manager. Please remove extra components.";

        public const string IcurrencyKeyBellowZeroError = "{0} error. Value can't be below zero.";

        public const string WalletManagerIsNotInitiatedError = "SimpleWallet Wallet Managet is not initiated. Looks like you have no Wallet Managet component on game scene.";
        public const string WalletManagerCurrencyKeyWasNotFound = "Currency with key {0} was not found in Wallet Manager. Please check your scripts or Wallet Manager component in Inspector";

    }
}