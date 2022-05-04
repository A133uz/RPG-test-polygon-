using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject UI;
    public GameObject panelShop;
    public Image portrait;
    public Text Name;
    public Text text;

    public Button[] answrs;

    public static bool isUiActive;
    public Dialogue curr;
     int repl;

    public static DialogueManager DMinstance;

    /* 
    Коды
    777 - магазин
    666 - конец диалога
    */
    private void Awake()
    {
        DMinstance = this;
    }
    void Access()
    {

    }

    private void Update()
    {
        if (curr != null)
        {
            UI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextRepl();
            }
        }
        else UI.SetActive(false);
    }

    private void NextRepl()
    {
        switch (repl)
        {
            case 666: ExitFromRepl(); break;
            case 777: ; break;
        }
    }

    private void ExitFromRepl()
    {
        throw new NotImplementedException();
    }
} 

    
