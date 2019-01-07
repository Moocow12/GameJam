using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Tutorial : MonoBehaviour
{
    public int CurrentStep = 0;
    //Initially Opening
    public GameObject S0Canvas;



    //Step 1 Objects
    public GameObject S1Canvas;
    public GameObject S1Helper;


    //Step 2 Objects
    public GameObject S2Canvas;


    //Step 3 Objects
    public GameObject S3Canvas;

    //Step 4 Objects
    public GameObject S4Canvas;
    public GameObject Launcher;

    //Step 5 Objects
    public Button CraftingTable;
    public GameObject S5Canvas;


    //Step 6 Objects 
    public GameObject S6Canvas;
    public GameObject S6Helper;
    // Update is called once per frame
    void Update()
    {
        if (CurrentStep == 0)
        {
            if (!S0Canvas.activeInHierarchy)
            {
                CurrentStep = 1;
                S1Canvas.SetActive(true);
                S1Helper.SetActive(true);
            }
        }

        if (CurrentStep == 1)
        {

            if (S1Helper == null)
            {
                S1Canvas.SetActive(false);
                CurrentStep = 2;
            }

        }

        if (CurrentStep == 2)
        {
            if (S2Canvas != null)
                S2Canvas.SetActive(true);
            Destroy(S2Canvas, 5f);
            if (S2Canvas == null)
            {
                CurrentStep = 3;
            }
        }
        if (CurrentStep == 3)
        {
            if (S3Canvas != null)
                S3Canvas.SetActive(true);
            Destroy(S3Canvas, 5f);
            if (S3Canvas == null)
            {
                CurrentStep = 4;
            }
        }
        if (CurrentStep == 4)
        {
            Launcher.SetActive(true);
            if (S4Canvas != null)
            {
                S4Canvas.SetActive(true);
                Destroy(S4Canvas, 5f);
            }
            if (S4Canvas == null)
            {
                CurrentStep = 5;
            }

        }

        if (CurrentStep == 5)
        {
            S5Canvas.SetActive(true);
            CraftingTable.enabled = true;

        }
        if(CurrentStep == 6)
        {
            if (S6Canvas != null)
            {
                S6Canvas.SetActive(true);
                Destroy(S6Canvas, 5f);
            }
            S6Helper.SetActive(true);
        }
    }


    public void SetStep(int step)

    {
        CurrentStep = step;
    }


    public void ChangeToLevel1()
    {
        SceneManager.LoadScene(1);
    }
}
