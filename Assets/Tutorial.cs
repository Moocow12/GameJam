using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Button CraftingTable;

    // Update is called once per frame
    void Update()
    {
        if(CurrentStep == 0)
        {
            if(!S0Canvas.activeInHierarchy )
            {
                CurrentStep = 1;
                S1Canvas.SetActive(true);
                S1Helper.SetActive(true);
            }
        }

        if (CurrentStep == 1)
        {

            if(S1Helper == null)
            {
                S1Canvas.SetActive(false);
                CurrentStep = 2;
            }

        }

        if(CurrentStep == 2)
        {
            Destroy(S2Canvas, 5f);
            if(S2Canvas == null)
            {
                CurrentStep = 3;
            }
        }
        if (CurrentStep == 3)
        {
            Destroy(S3Canvas, 5f);
            if (S3Canvas == null)
            {
                CurrentStep = 4;
            }
        }

        if (CurrentStep == 4)
        {
            CraftingTable.enabled = true;

            
        }
    }



   

}
