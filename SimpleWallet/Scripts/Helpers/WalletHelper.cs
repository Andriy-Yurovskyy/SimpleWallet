namespace SimpleWallet.Helpers
{
    using System;
    using UnityEditor;
    using UnityEngine;

    public class WalletHelper
    {

        public static bool IsEmptyString(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return true;
            }
            return String.IsNullOrEmpty(s.Trim());
        }


        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }


        public static void DisplayDelimiter()
        {
            EditorGUILayout.Space();
        }


        public static void DisplayHorizontalLine()
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }


        public static int GetEnumValuesCount (Type e)
        {
            return Enum.GetNames(e).Length;
        }


    }
}


