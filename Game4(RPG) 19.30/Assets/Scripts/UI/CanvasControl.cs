using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    public GameObject MainPan;
    bool isOpen;

    private void Awake()
    {
        MainPan = transform.GetChild(0).gameObject;
        MainPan.SetActive(false);
        
    }
    private void Update()
    {
        OpenInvHUD();
    }

    void OpenInvHUD()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Time.timeScale = 0;
            isOpen = !isOpen;
            MainPan.SetActive(isOpen);
            Time.timeScale = 1;
            

        }
       
    }
}
