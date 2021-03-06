﻿using UnityEngine;
using UnityEngine.UI;

namespace Translation
{
    public class TranslateText : MonoBehaviour
    {
        private bool _isInstancied = false;
        private string _key = string.Empty;
        private object[] _parameters = null;

        private void Awake()
        {
            _key = GetComponent<Text>().text;
            TextManager.OnLanguageLoaded += OnLanguageLoaded;
        }

        private void OnEnable()
        {
            if (_isInstancied)
                return;

            _isInstancied = true;
            ChangeText();
        }

        private void OnDestroy()
        {
            TextManager.OnLanguageLoaded -= OnLanguageLoaded;
        }

        private void OnLanguageLoaded(bool firstLaunch)
        {
            ChangeText();
        }

        private void ChangeText()
        {
            GetComponent<Text>().text = _parameters == null ? TextManager.GetText(_key) : TextManager.GetTextWithParameters(_key, _parameters);
        }

        public void SetText(string key, object[] parameters = null, bool changeText = true)
        {
            _key = key;
            _parameters = parameters;

            if (changeText)
                ChangeText();
        }
    }
}