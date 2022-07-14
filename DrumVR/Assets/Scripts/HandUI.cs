using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandUI : MonoBehaviour
{
    GameObject handUI;
    GameObject interactor;
    [SerializeField]
    GameObject[] sticks;

    bool isUIActive = true;
    
    void Start()
    {
        handUI = GameObject.Find("Hand UI");
        interactor = GameObject.Find("Interactor");
        sticks = GameObject.FindGameObjectsWithTag("Stick");

        ToggleUI();
    }

    public void ThumbClicked(InputAction.CallbackContext context)
    {
        if(context.performed)
            ToggleUI();
    }

    public void ToggleUI()
    {
        if(isUIActive)
        {   
            isUIActive = false;
            handUI.SetActive(false);
            interactor.SetActive(false);
            foreach (GameObject stick in sticks)
                stick.SetActive(true);
        }
        else
        {
            isUIActive = true;
            handUI.SetActive(true);
            interactor.SetActive(false);
            foreach (GameObject stick in sticks)
                stick.SetActive(false);
        }
    }

}
