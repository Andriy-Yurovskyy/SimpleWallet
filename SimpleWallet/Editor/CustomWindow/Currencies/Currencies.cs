namespace SimpleWallet.Editor.CustomWindow.Currencies
{
    using System;
    using System.Collections.Generic;
    using SimpleWallet.Currencies.Enums;
    using SimpleWallet.StringConstans;
    using SimpleWallet.ErrorsHandler;
    using SimpleWallet.Helpers;
    using UnityEditor;
    using UnityEngine;

    public class Currencies
    {
        private static bool displayCurrenciesKeysInfo = true;

        public List<string> currencyKeys {get; private set;}


        public void DisplayCurrenciesInEditor()
        {
            DisplayHelpInfo();
            WalletHelper.DisplayDelimiter();
            DisplayKeysSettings();
            DisplayAddNewKeyButton();
            WalletHelper.DisplayDelimiter();
        }


        public void LoadFromEnum()
        {
            currencyKeys = new List<string>();
            foreach (CurrencyType item in (CurrencyType[])Enum.GetValues(typeof(CurrencyType)))
            {
                currencyKeys.Add(item.ToString());
            }
        }


        private void DisplayHelpInfo()
        {
            displayCurrenciesKeysInfo = EditorGUILayout.Foldout(displayCurrenciesKeysInfo, WalletTexts.CurrenciesKeysInformationLableName);
            if (!displayCurrenciesKeysInfo)
            {
                return;
            }
            EditorGUILayout.HelpBox(WalletTexts.CurrencyTypesExplanation, MessageType.None);
        }


        private void DisplayKeysSettings()
        {
            EditorGUILayout.LabelField(WalletTexts.CurrenciesKeysSetupLableName);

            for (var i = 0; i < currencyKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(String.Format(WalletTexts.CurrencyKeyLable, i), GUILayout.MaxWidth(40));
                currencyKeys[i] = EditorGUILayout.TextField(GetValidatedValue(i), GUILayout.MinWidth(200));

                bool shoudRemoveKey = GUILayout.Button(WalletTexts.RemoveCurrencyKeyButtonName, GUILayout.MaxWidth(70));
                EditorGUILayout.EndHorizontal();
                DisplayKeyValueErrorInEditor(i);
                RemoveKey(shoudRemoveKey, i);
            }
        }


        private void DisplayAddNewKeyButton()
        {
            if (!GUILayout.Button(WalletTexts.AddNewcurrencyNameButtonKey))
            {
                return;
            }
            AddNewKey();
        }


        private void AddNewKey()
        {
            for (int i = 0; i < currencyKeys.Count; i++)
            {
                if (WalletHelper.IsEmptyString(currencyKeys[i]))
                {
                    EditorUtility.DisplayDialog(WalletTexts.CurrencyKeyEmptyKeyErrorDialogTitle, String.Format(WalletTexts.CurrencyKeyEmptyKeyErrorDialogText, i), WalletTexts.CurrencyKeyEmptyKeyErrorDialogOkButtonText);
                    return;
                }
            }

            currencyKeys.Add(default(string));
        }


        private void RemoveKey(bool removeFlag, int i)
        {
            if (!removeFlag)
            {
                return;
            }

            if (WalletHelper.IsEmptyString(currencyKeys[i]))
            {
                currencyKeys.RemoveAt(i);
                return;
            }

            bool confirmRemove = EditorUtility.DisplayDialog(WalletTexts.RemoveCurrencyKeyConfirmDialogTitle, WalletTexts.RemoveCurrencyKeyConfirmDialogText, WalletTexts.RemoveCurrencyKeyConfirmDialogOkButtonText, WalletTexts.RemoveCurrencyKeyConfirmDialogCancelButtonText);

            if (!confirmRemove)
            {
                return;
            }
            currencyKeys.RemoveAt(i);
        }


        private void DisplayKeyValueErrorInEditor(int i)
        {
            string value = currencyKeys[i];
            string errorString;
            if (WalletHelper.IsEmptyString(value))
            {
                return;
            }

            if (ErrorsHandler.IsMatchPatternError(value, out errorString))
            {
                EditorGUILayout.HelpBox(errorString, MessageType.Error);
                return;
            }

            if (ErrorsHandler.IsDublicatedError(currencyKeys, value, i, out errorString))
            {
                EditorGUILayout.HelpBox(errorString, MessageType.Error);
                return;
            }

        }


        private string GetValidatedValue(int i)
        {
            string value = currencyKeys[i];
            if (WalletHelper.IsEmptyString(value))
            {
                return currencyKeys[i];
            }
            return WalletHelper.UppercaseFirst(value);
        }


    }
}