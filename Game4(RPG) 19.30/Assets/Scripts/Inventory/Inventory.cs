using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instanceI;
    

    public Item[] items;
    public int[] count;
    public Item[] equipment;
    GameObject dropPrefab;

    public int money;

    public int arrowID;

    private void Awake()
    {
        instanceI = this;
        items = new Item[30];
        
        count = new int[items.Length];
        equipment = new Item[3];
        dropPrefab = Resources.Load<GameObject>("Prefabs/Someitem");
    }
    #region ItemInteract
    public bool Use(int id)
    {
        if (!items[id]) return false;
        switch (items[id].type)
        {
            case Item.ItemType.item: return UseItem(id);
            
            default: SetEquip(items[id].type, id);
                return true;
        }
    }
    private bool UseItem(int id)
    {
        if (!items[id].isUseful) return false;
        if (count[id] > 1)
        {

            count[id]--;
        }
        else
        {
            count[id] = 0;
            items[id] = null;
        }
        return true;
    }

    private void SetEquip(Item.ItemType equip,int id)
    {
        if (equipment[(int)equip] == items[id]) equipment[(int)equip] = null;
        else equipment[(int)equip] = items[id];
    }
    #endregion


    #region ItemAction
    public bool AddItem(Item newItem, int newCount)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] && newItem.id == items[i].id)
            {
                count[i] += newCount;
                return true;
            }
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                count[i] = newCount;
                return true;
            }
        }
        return false;
    }
    
    public void MoveItem(int oldID, int newID)
    {
        items[newID] = items[oldID];
        count[newID] = count[oldID];

        items[oldID] = null;
        count[oldID] = 0;
    }

    public void SwapItems(int oldID, int newID)
    {
        Item tmpItm = items[newID];
        int tmpCnt = count[newID];

        items[newID] = items[oldID];
        count[newID] = count[oldID];

        items[oldID] = tmpItm;
        count[oldID] = tmpCnt;
    }

    public void DropItem(CellScript previous)
    {
        int index = (int)items[previous.cellID].type;
        if (equipment.Length > index)
        {
            if (equipment[index] == items[previous.cellID])
            {
                equipment[index] = null;
                previous.Equiped = false;

            }
        }
        ItemSett dItem =  Instantiate(dropPrefab, transform.position, Quaternion.identity).GetComponent<ItemSett>();
         dItem.item = items[previous.cellID];
         dItem.count = count[previous.cellID];

        items[previous.cellID] = null;
        count[previous.cellID] = 0;


    }
    #endregion

    public bool ArrowCheck(int id)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                if (items[i].id == id)
                {
                    arrowID = i;
                    return true;
                }
            }
        }
        return false;
    }

    public void ArrowUse()
    {

        count[arrowID]--;
        InventoryRefresh();
    }

    private void InventoryRefresh()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (count[i] <= 0)
            {
                items[i] = null;
            }
        }
    }
}
