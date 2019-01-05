using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MessageDisplay : MonoBehaviour {



    public static MessageDisplay Instance;
    TextMeshProUGUI _text;
    

    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void Awake()
    {
        Singleton();
    }
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.enabled = false;
    }




    public void DisplayMessage(string message, float timeDisplayed)
    {
        _text.text = message;
        StopAllCoroutines();
        StartCoroutine(DisplayMessage(timeDisplayed));
    }


    IEnumerator DisplayMessage(float time)
    {
        _text.enabled = true;
        yield return new WaitForSeconds(time);
        _text.enabled = false;
    }

}
