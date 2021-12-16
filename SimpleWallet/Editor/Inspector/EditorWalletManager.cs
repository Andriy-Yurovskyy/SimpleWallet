namespace SimpleWallet.Editor.Inspector
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using SimpleWallet;
    using SimpleWallet.Currencies.Structs;
    using SimpleWallet.Currencies.Enums;
    using System;
    using SimpleWallet.StringConstans;
    using System.Linq;
    using SimpleWallet.ErrorsHandler;
    using SimpleWallet.Helpers;
    using SimpleWallet.Editor.Extensions;
    using SimpleWallet.SaveBalance.Enums;

    [CustomEditor(typeof(WalletManager))]
    public class EditorWalletManager : Editor
    {

        SerializedProperty currenciesAttributes;

        private List<CurrencyAttributes> currencies;

        private bool displayWalletCurrencies = true;

        SerializedProperty saveLoadSettingsType;

        SaveSettingsType saveLoadSettings;

        private bool currencyKeysErrorFlag;

        private List<string> keyStrings;

        private List<bool> displayCurrenciesKeys;

        private bool isOnlyObjectOnScene;


        private void OnEnable()
        {
            CheckOneWalletManagerOnScene();
            if (!IsOnlyOneWalletManager())
            {
                return;
            }
            SetupObjects();
            SettupDisplayCurrenciesKeys();

        }
         

        public override void OnInspectorGUI()
        {
            if (!IsOnlyOneWalletManager())
            {
                DisplayIsNotOnlyOneManagerErrorText();
                return;
            }

            UpdateCurrenciesKeysStrings();
            DisplayHeader();
            if (!IsCurrenciesKeysSetup())
            {
                return;
            }

            DisplayCurrencies();
            DisplaySaveBalanceSettings();
            DisplayFooter();
        }


        private void DisplayFooter()
        {
            WalletHelper.DisplayDelimiter();

            GUILayout.BeginHorizontal();
                DisplayDiscardChangesButton();
                DisplaySaveChangesButton();
            GUILayout.EndHorizontal();

            DisplayUnfavedChangesInfo();

            WalletHelper.DisplayDelimiter();
        }


        private void DisplayHeader()
        {


            WalletHelper.DisplayDelimiter();

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                    DisplayOpenSettingsButton();
                    DisplayAddNewCurrencyButton();
                GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            WalletHelper.DisplayDelimiter();

            if (!IsCurrenciesKeysSetup())
            {
                DisplaySetupCurrencyKeysWarning();
                return;
            }
        }


        private void DisplayCurrency(int i)
        {
            DisplayCurencyKeyFoldOut(i);
            if (IsCurrencyFolded(i))
            {
                return;
            }

            CurrencyAttributes currency = currencies[i];

            GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                    GUILayout.Label(WalletTexts.WalletManagerInspectorCurrencyNameLabel);
                    currency.Name = EditorGUILayout.TextField(currency.Name, GUILayout.MinWidth(200));
                    GUILayout.Label(WalletTexts.WalletManagerInspectorCurrencyKeyLabel);
                    currency.Key = (CurrencyType)EditorGUILayout.EnumPopup(currency.Key, GUILayout.MinWidth(200));
                GUILayout.EndVertical();
                GUILayout.BeginVertical();
                    GUILayout.Label(WalletTexts.WalletManagerInspectorCurrencyIconLabel, GUILayout.MaxWidth(100));
                currency.Icon = (Sprite)EditorGUILayout.ObjectField("", currency.Icon, typeof(Sprite), true, GUILayout.MaxWidth(100));
                GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            currencies[i] = currency;

            DisplayCurrencyDublicatedError(i);
            DisplayCurrencyRemoveButton(i);

            WalletHelper.DisplayHorizontalLine();

            if (i != currencies.Count - 1)
            {
                WalletHelper.DisplayDelimiter();
            }
        }


        private void DisplayCurrencies()
        {
            displayWalletCurrencies = EditorGUILayout.Foldout(displayWalletCurrencies, WalletTexts.WalletManagerInspectorCurrenciesLabel, true, FoldoutStyle());
            if (displayWalletCurrencies)
            {
                currencyKeysErrorFlag = false;
                for (int i = 0; i < currencies.Count; i++)
                {
                    DisplayCurrency(i);
                }
            }
        }


        private void DisplaySaveBalanceSettings()
        {
            saveLoadSettings = (SaveSettingsType)EditorGUILayout.EnumPopup(WalletTexts.WalletManagerInspectorSaveBalanceLabel, saveLoadSettings);
        }


        private void SetupObjects()
        {
            currencyKeysErrorFlag = false;
            WalletManager walletManager = (WalletManager)target;
            var serializedObject = new SerializedObject(walletManager);
            currenciesAttributes = serializedObject.FindProperty("availableCurrencies");
            saveLoadSettingsType = serializedObject.FindProperty("saveLoadSettingsType");
            saveLoadSettings = LoadSaveWalletBalanceSerializedValues();
            currencies = LoadCurrenciesSerializedValues();
        }

        private void DisplayCurencyKeyFoldOut(int i)
        {
            displayCurrenciesKeys[i] = EditorGUILayout.Foldout(displayCurrenciesKeys[i], String.Format(WalletTexts.WalletManagerInspectorCurrencyKeyFoldoutLabel, i, currencies[i].Key), true, FoldoutStyle());
        }


        private bool IsCurrencyFolded(int i)
        {
            return !displayCurrenciesKeys[i];
        }


        private void RemoveCurrency(int index)
        {
            currencies.RemoveAt(index);
        }


        private bool IsCurrenciesKeysSetup()
        {
            if (WalletHelper.GetEnumValuesCount(typeof(CurrencyType)) == 0)
            {
                return false;
            }
            return true;
        }


        private void DisplaySetupCurrencyKeysWarning()
        {
            EditorGUILayout.HelpBox(String.Format(WalletTexts.WalletManagerInspectorKeysNotSetupErrorText, WalletTexts.PackageEditorWindowPath), MessageType.Error);
        }


        private void DisplayOpenSettingsButton()
        {

            bool isSettingsClicked = GUILayout.Button(WalletTexts.WalletManagerInspectorOpenSettingsButton);
            if (!isSettingsClicked)
            {
                return;
            }

            WalletEditorSettings.ShowWindow();
        }


        private void DisplayCurrencyRemoveButton(int i)
        {
            bool removeKey = GUILayout.Button(String.Format(WalletTexts.WalletManagerInspectorCurrencyRemoveCurrencyButton, i, currencies[i].Key));
            if (!removeKey)
            {
                return;
            }

            RemoveCurrency(i);
        }


        private void DisplayAddNewCurrencyButton()
        {
            if (!IsCurrenciesKeysSetup())
            {
                return;
            }

            bool clicked = GUILayout.Button(WalletTexts.WalletManagerInspectorAddCurrencyButton);

            if (!clicked)
            {
                return;
            }

            if (currencyKeysErrorFlag)
            {
                Debug.LogError(WalletTexts.WalletManagerInspectorAddCurrencyErrorErrorsInEditorText);
                return;
            }

            if (currencies.Count >= WalletHelper.GetEnumValuesCount(typeof(CurrencyType)))
            {
                Debug.LogError(WalletTexts.WalletManagerInspectorAddCurrencyErrorLimitText);
                return;
            }

            currencies.Add(new CurrencyAttributes());
            SettupDisplayCurrenciesKeys();
        }


        private void DisplayCurrencyDublicatedError(int i)
        {
            string errorText;
            if (ErrorsHandler.IsDublicatedError(keyStrings, currencies[i].Key.ToString(), i, out errorText))
            {
                EditorGUILayout.HelpBox(String.Format(errorText, i), MessageType.Error);
                currencyKeysErrorFlag = true;
            }
        }


        private void DisplayDiscardChangesButton()
        {
            bool clicked = GUILayout.Button(WalletTexts.WalletManagerInspectorDiscardChangesButtonText);
            if (!clicked)
            {
                return;
            }

            if (!UnsavedChanges())
            {
                Debug.Log(WalletTexts.WalletManagerInspectorNoChangesToDiscardText);
            }

            currencies = LoadCurrenciesSerializedValues();
        }


        private void DisplaySaveChangesButton()
        {
            bool clicked = GUILayout.Button(WalletTexts.WalletManagerInspectorSaveChangesButtonText);

            if (!clicked)
            {
                return;
            }

            if (currencyKeysErrorFlag)
            {
                Debug.LogError(WalletTexts.WalletManagerInspectorSaveChangesErrorErrorsInEditorText);
                return;
            }

            if (!UnsavedChanges())
            {
                Debug.Log(WalletTexts.WalletManagerInspectorNoChangesToSaveText);
                return;
            }

            currenciesAttributes.SetValue(currencies.ToArray());
            saveLoadSettingsType.SetValue<Enum>(saveLoadSettings);
            EditorUtility.SetDirty(target);

            Debug.Log(WalletTexts.WalletManagerInspectorChangesSuccessfullySavedText);
        }


        private void DisplayUnfavedChangesInfo()
        {
            if (UnsavedChanges() && !currencyKeysErrorFlag)
            {
                EditorGUILayout.HelpBox(WalletTexts.WalletManagerInspectorUnsavedChangesText, MessageType.Info);
            }
        }


        private void SettupDisplayCurrenciesKeys()
        {
            displayCurrenciesKeys = new List<bool>();
            for (int i = 0; i < currencies.Count; i++)
            {
                displayCurrenciesKeys.Add(true);
            }
        }


        private List<CurrencyAttributes> LoadCurrenciesSerializedValues()
        {
            List<CurrencyAttributes> result = new List<CurrencyAttributes>();
            CurrencyAttributes[] currencyAttributesValue = currenciesAttributes.GetValue<CurrencyAttributes[]>();
            if (currencyAttributesValue != null)
            { 
                result =  currenciesAttributes.GetValue<CurrencyAttributes[]>().ToList();
            }
            return result;
        }

        private SaveSettingsType LoadSaveWalletBalanceSerializedValues ()
        {
            return (SaveSettingsType)saveLoadSettingsType.GetValue<Enum>();
        }


        private bool UnsavedChanges()
        {
            if (!Enumerable.SequenceEqual(LoadCurrenciesSerializedValues(), currencies))
            {
                return true; 
            }

            if (saveLoadSettings != LoadSaveWalletBalanceSerializedValues())
            {
                return true;
            }

            return false;
        }

         
        private void UpdateCurrenciesKeysStrings()
        {
            keyStrings = new List<string>();
            for (int i = 0; i < currencies.Count; i++)
            {
                keyStrings.Add(currencies[i].Key.ToString());
            }
        }


        private GUIStyle FoldoutStyle()
        {
            GUIStyle result = new GUIStyle(EditorStyles.foldout);
            result.fontStyle = FontStyle.Bold;
            return result;
        }


        private void CheckOneWalletManagerOnScene()
        {
            isOnlyObjectOnScene = GameObject.FindObjectsOfType<WalletManager>().Length == 1;
        }


        private bool IsOnlyOneWalletManager()
        {
            return isOnlyObjectOnScene;
        }


        private void DisplayIsNotOnlyOneManagerErrorText()
        {

            EditorGUILayout.HelpBox(WalletTexts.WalletManagerInspectorIsNotOnlyOneManagerErrorText, MessageType.Error);
        }


    }
}