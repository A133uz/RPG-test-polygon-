using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum whaEquip
{
    melee = 0, distance = 1, armor = 2
}

public class EquipCell : MonoBehaviour
{
    public whaEquip equipCell;
    public void Refresh()
    {
        if (Inventory.instanceI.equipment[(int)equipCell])
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(1).GetComponent<Image>().sprite = Inventory.instanceI.equipment[(int)equipCell].mySpr;
        }
        else transform.GetChild(1).gameObject.SetActive(false);
    }

}
