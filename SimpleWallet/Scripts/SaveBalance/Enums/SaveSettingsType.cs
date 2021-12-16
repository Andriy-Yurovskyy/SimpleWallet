namespace SimpleWallet.SaveBalance.Enums
{
    using System;

    [Serializable]
    public enum SaveSettingsType
    {
        UserPrefs,
        BinaryFile,
        TextFile,
    }
}