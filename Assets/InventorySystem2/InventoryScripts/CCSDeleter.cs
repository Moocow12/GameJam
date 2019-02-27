using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCS
{
    public class CCSDeleter : MonoBehaviour
    {
        public static CCSDeleter Instance;
        public GameObject UIHolder;
        public Button deleteButton, cancelButton;

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


        private void Awake()
        {
            Singleton();
            UIHolder.SetActive(false);
        }

        private void Start()
        {
            InitializeButtons();
        }

        public  void InitializeButtons()
        {
            deleteButton.onClick.AddListener(() => Delete());
            cancelButton.onClick.AddListener(() => Close());
        }

        public void OpenDeleter()
        {
            UIHolder.SetActive(true);
        }

        protected void Delete()
        {
            CCSInventoryManager.Instance.DeleteItems();
            Close();
        }
        
        protected void Close()
        {
            UIHolder.SetActive(false);
        }

        public bool IsEnabled
        {
            get { return UIHolder.activeInHierarchy; }
        }

    }
}
