using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Translation
{
    public class TextManager : MonoBehaviour
    {
        #region Events

        public delegate void LanguageLoadedEvent(bool firstLaunch);

        public static event LanguageLoadedEvent OnLanguageLoaded;

        #endregion Events

        private static Hashtable _textTable;

        private static bool _firstLaunch = true;

        private void Start()
        {
            if (PlayerPrefs.HasKey("language"))
            {
                if (!LoadLanguage(PlayerPrefs.GetString("language")))
                {
                    LoadLanguage("English");
                }
            }
            else
            {
                LoadLanguage("English");
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void ChangeLanguage(string language)
        {
            if (LoadLanguage(language))
            {
                PlayerPrefs.SetString("language", language);
            }
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
        {
            OnLanguageLoaded?.Invoke(false);
        }

        public static bool LoadLanguage(string filename)
        {
            if (filename == null)
            {
                _textTable = null;
                return false;
            }

            string filePath = Path.Combine(Path.Combine(Application.streamingAssetsPath, "Languages"), filename + ".po");
            string textAsset;

            try
            {
                textAsset = File.ReadAllText(filePath);
            }
            catch (Exception)
            {
                return false;
            }

            if (_textTable == null)
            {
                _textTable = new Hashtable();
            }

            _textTable.Clear();

            StringReader reader = new StringReader(textAsset);
            string key = null;
            string val = null;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("msgid \""))
                {
                    key = line.Substring(7, line.Length - 8);
                }
                else if (line.StartsWith("msgstr \""))
                {
                    val = line.Substring(8, line.Length - 9);
                }
                if (key != null && val != null)
                {
                    _textTable.Add(key, val);
                    key = null;
                    val = null;
                }
            }
            reader.Close();

            OnLanguageLoaded?.Invoke(_firstLaunch);
            _firstLaunch = false;

            return true;
        }

        public static string GetTextWithParameters(string key, object[] parameters)
        {
            if (parameters == null)
                parameters = new object[0];

            if (key != null && _textTable != null)
            {
                if (_textTable.ContainsKey(key))
                {
                    string result = (string)_textTable[key];
                    if (result.Length > 0)
                    {
                        return string.Format(result, parameters);
                    }
                }
            }
            return string.Format(key, parameters);
        }

        public static string GetText(string key)
        {
            if (key != null && _textTable != null)
            {
                if (_textTable.ContainsKey(key))
                {
                    string result = (string)_textTable[key];
                    if (result.Length > 0)
                    {
                        return result;
                    }
                }
            }
            return key;
        }
    }
}