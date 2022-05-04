using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CellScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int cellID = 0;
    private InventoryUI invUi;

    public bool isFree = true;
    public bool Equiped = false;

    private Image myImg;
    private Text myTxt;

    public void Refresh()
    {
        if (Inventory.instanceI.items[cellID])
        {
            isFree = false;
            myImg.gameObject.SetActive(true);
            myTxt.gameObject.SetActive(true);
            myImg.sprite = Inventory.instanceI.items[cellID].mySpr;
            myTxt.text = Inventory.instanceI.count[cellID].ToString();
        }
        else
        {
            isFree = true;
            myImg.gameObject.SetActive(false);
            myTxt.gameObject.SetActive(false);
        }
    }
    public CellScript GetLinkSetSettings(int id, InventoryUI newUI)
    {
        cellID = id;
        invUi = newUI;
        isFree = true;
        myImg = transform.GetChild(1).GetComponent<Image>();
        myTxt = transform.GetChild(2).GetComponent<Text>();
        return this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (invUi) invUi.CursorCellSwitch(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (invUi) invUi.CursorCellSwitch(this);
    }
}
