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



    /// <summary>
    /// Displays the message on the screen for a designated amount of time.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="timeDisplayed"></param>
    public void DisplayMessage(string message, float timeDisplayed)
    {
        _text.text = message;
        StopAllCoroutines();
        StartCoroutine(DisplayMessage(timeDisplayed));
    }

    /// <summary>
    /// Is the time counter for the message.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator DisplayMessage(float time)
    {
        _text.enabled = true;
        yield return new WaitForSeconds(time);
        _text.enabled = false;
    }

    /// <summary>
    /// Displays the message for the alloted time if in the Unity editor.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="timeDisplayed"></param>
    public void EditorMessage(string message, float timeDisplayed)
    {
        if(Application.isEditor)
        {
            DisplayMessage(message, timeDisplayed);
        }
        
    }
}
