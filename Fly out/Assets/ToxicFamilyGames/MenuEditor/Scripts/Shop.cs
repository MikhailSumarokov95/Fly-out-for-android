using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

//PlayerPrefs money
namespace ToxicFamilyGames.MenuEditor
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private UnityEvent<int> onBuySkin;
        [SerializeField] private Transform itemTransform;
        [SerializeField] private Item[] items;
        [SerializeField] private TMP_Text priceText;
        [SerializeField, Header("Кнопки")] private GameObject buyButton;
        [SerializeField] private GameObject selectButton;


        private int indexShowingItem = 0;
        public static int Money
        {
            get { return PlayerPrefs.GetInt("money", 0); }
            set { PlayerPrefs.SetInt("money", value); } }

        public bool IsBuySelectedItem
        {
            get { return PlayerPrefs.GetInt(items[indexShowingItem].gameObject.name, 0) == 1; }
        }

        public void Start()
        {
            InitItem();
            InitButtons();
            PlayerPrefs.SetInt(items[0].gameObject.name, 1);
            indexShowingItem = PlayerPrefs.GetInt("selectedItem", 0);
            ShopersOnSelect();
        }

        [ContextMenu("NextItem")]
        public void NextItem()
        {
            indexShowingItem = (indexShowingItem + 1) % items.Length;
            InitItem();
        }

        [ContextMenu("PreviousItem")]
        public void PreviousItem()
        {
            indexShowingItem = (indexShowingItem - 1 + items.Length) % items.Length;
            InitItem();
        }

        private void InitItem()
        {
            DestroyItem();
            GameObject item = Instantiate(items[indexShowingItem].gameObject, itemTransform);
            priceText.text = items[indexShowingItem].price.ToString();
            MonoBehaviour[] components = item.GetComponents<MonoBehaviour>();
            for (int i = 0; i < components.Length; i++)
            {
                components[i].enabled = false;
            }
            item.transform.localScale *= items[indexShowingItem].scaleRatio;
            item.AddComponent<RotatingItem>();
            InitButtons();
        }

        private void InitButtons()
        {
            bool isBuy = IsBuySelectedItem;
            selectButton.SetActive(isBuy);
            buyButton.SetActive(!isBuy);
        }

        [ContextMenu("DestroyItem")]
        private void DestroyItem()
        {
            if (itemTransform.childCount != 0) Destroy(itemTransform.GetChild(0).gameObject);
        }

        [ContextMenu("BuyItem")]
        public void BuyItem()
        {
            if (Money - items[indexShowingItem].price >= 0)
            {
                onBuySkin?.Invoke(items[indexShowingItem].price);
                PlayerPrefs.SetInt(items[indexShowingItem].gameObject.name, 1);
                InitButtons();
                SelectItem();
            }
        }

        [ContextMenu("SelectItem")]
        public void SelectItem()
        {
            PlayerPrefs.SetInt("selectedItem", indexShowingItem);
            ShopersOnSelect();
        }
        private void ShopersOnSelect()
        {
            IShoper[] shopers = FindObjectsOfType<MonoBehaviour>().OfType<IShoper>().ToArray();
            Array.ForEach(shopers, shoper => { shoper?.OnSelect(items[indexShowingItem].gameObject); });
        }
    }
}
