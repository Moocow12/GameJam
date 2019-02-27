using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCS {
    public class CCSSplitter : MonoBehaviour
    {

        #region Singleton
        public static CCSSplitter Instance;

        void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        #endregion


        //Required Components for functionality of the splitter.
        public GameObject uiHolder;
        public CCSSlot displaySlot;
        public Button upButton, downButton, splitButton, cancelButton;
        CCSSlot tempSlot;

        

        private void Awake()
        {
            Singleton();
            uiHolder.SetActive(false);
        }

        
        void Start()
        {
            InitializeButtons();
        }



        /// <summary>
        /// Changes the listeners of the buttons to their designated functionality.
        /// </summary>
        public void InitializeButtons()
        {
            upButton.onClick.AddListener(() => IncreaseAmount());
            downButton.onClick.AddListener(() => DecreaseAmount());
            splitButton.onClick.AddListener(() => Split());
            cancelButton.onClick.AddListener(() => Close());
        }



        /// <summary>
        /// Used within the CCSInventoryManager to determine what functions are available to the slots if the splitter is open.
        /// </summary>
        public bool IsEnabled
        {
            get{ return uiHolder.activeInHierarchy; }
        }

        /// <summary>
        /// Initializes the splitter with all of the required information to change the item.
        /// </summary>
        /// <param name="slot"></param>
        public void OpenSplitter(CCSSlot slot)
        {
            if (slot.ItemCount() > 1) { 
            tempSlot = slot;
            uiHolder.SetActive(true);
            displaySlot.ClearSlot();
            displaySlot.AddItem(tempSlot.CheckItem);
            }

        }

        /// <summary>
        /// Closes the splitter.
        /// </summary>
        protected void Close()
        {
            uiHolder.SetActive(false);
        }


        /// <summary>
        /// Increases the amount of items that are in the display slot.
        /// </summary>
        protected  void IncreaseAmount()
        {
           if(displaySlot.ItemCount() < tempSlot.ItemCount())
            {
                displaySlot.AddItem(tempSlot.CheckItem);
            }
        }

        /// <summary>
        /// Decreases the amount of items that are in the display slot.
        /// </summary>
        protected void DecreaseAmount()
        {
            if(displaySlot.ItemCount()>1)
            {
                displaySlot.RemoveItem();
            }
        }

        /// <summary>
        /// Takes the number of items that are within the display slot and tells the CCSInventoryManager how many items need to be moved.
        /// </summary>
        protected void Split()
        {
            CCSInventoryManager.Instance.SplitItems(displaySlot.ItemCount());
            Close();
        }
    }
}
