using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    public GameObject[] panels;

    private StatsUI stats;
    private InventoryUI invUI;
    private SpellBookUI spellBook;


    private void Awake()
    {
        Access();
    }
    private void Access()
    {
        panels = new GameObject[3];
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i] = transform.GetChild(i).gameObject;
        }
        stats = panels[0].GetComponent<StatsUI>();
        invUI = panels[1].GetComponent<InventoryUI>();
        spellBook = panels[2].GetComponent<SpellBookUI>();

        invUI.Access();
        gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        if (invUI) invUI.Cleaner();
    }
    private void OnDisable()
    {
        if (invUI) invUI.Cleaner();
    }
    //Buttons
}
