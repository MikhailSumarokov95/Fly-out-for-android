using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ToxicFamilyGames.MenuEditor
{
    public class Text : MonoBehaviour
    {
        [SerializeField]
        private string[] languages;
        private TMP_Text text;
        private float value;
        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            FindObjectOfType<LanguageController>().InitText(this);
            SetValue(value);
        }

        public void SetLanguage(int selectedLanguage)
        {
            text.text = languages[selectedLanguage];
        }

        public void SetValue(float value)
        {
            FindObjectOfType<LanguageController>().InitText(this);
            text.text = string.Format(text.text, this.value = value);
        }
    }
}