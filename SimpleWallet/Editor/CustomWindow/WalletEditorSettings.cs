namespace SimpleWallet.Editor
{
    using SimpleWallet.StringConstans;
    using UnityEditor;
    using UnityEngine;
    using SimpleWallet.Editor.CustomWindow.Currencies;
    using SimpleWallet.Editor.CustomWindow.SaveEnumTofile;
    using System;

    public class WalletEditorSettings : EditorWindow
    {
        Currencies editorCurrencies;

        [MenuItem(WalletTexts.PackageEditorWindowPath)]

        public static void ShowWindow()
        {
            GetWindow<WalletEditorSettings>(WalletTexts.PackageEditorWindowName);
        }


        private void OnEnable()
        {
            editorCurrencies = new Currencies();
            editorCurrencies.LoadFromEnum();
        }


        private void OnGUI()
        {
            DisplayWindowName();

            if (!DisplayNoWalletManagerOnScheneWarning())
            {
                return;
            }

            editorCurrencies.DisplayCurrenciesInEditor();
            EditorGUILayout.Space();
            DisplaySaveSettingControlButtons();
        }


        private void DisplayWindowName()
        {
            EditorGUILayout.LabelField(WalletTexts.PackageEditorWindowName, EditorStyles.boldLabel);
        }


        private void DisplaySaveSettingControlButtons()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(WalletTexts.DiscardWalletSettingsButtonName))
            {
                editorCurrencies.LoadFromEnum();
            }

            if (GUILayout.Button(WalletTexts.SaveWalletSettingsButtonName))
            {
                bool errorFlag = false;
                SaveEnum currencyKeysSaver = new SaveEnum(editorCurrencies.currencyKeys);
                errorFlag = currencyKeysSaver.CheckForErrors();

                if (errorFlag)
                {
                    return;
                }

                currencyKeysSaver.Save();

                Debug.Log(WalletTexts.SaveWalletSettingsSuccessMessage);

                AssetDatabase.Refresh();

            }
            EditorGUILayout.EndHorizontal();
        }


        private bool DisplayNoWalletManagerOnScheneWarning()
        {
            if (GameObject.FindObjectsOfType<WalletManager>().Length != 1)
            {
                EditorGUILayout.HelpBox(String.Format(WalletTexts.WalletManagerIsNotInitiatedError, WalletTexts.PackageEditorWindowPath), MessageType.Error);
                return false;
            }
            return true;
        }


    }
}