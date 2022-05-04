using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public CellScript[] cells;
    private Transform invCellPan;
    public Transform cursor;
    private Image cursorImage;
    private Text cursorText;
    [Header("Interactive Setts")]
    public Item selectedItem;
    public int selectedCount;
    public CellScript cursorCell;
    public CellScript selectedCell;
    public CellScript prevCell;

    ///Info Panel - Header



    [Header("Equip Panel")]
    private Transform equipPan;
    private EquipCell[] eqCells;



    public Color myColor;
    public Color cursorColor;
    public Color selectedColor;
    public Color[] equipColor;

    public void Access()
    {
        invCellPan = transform.GetChild(0);
        equipPan = transform.GetChild(2);
        
        if (cursor)
        {
            cursorImage = cursor.GetComponent<Image>();
            cursorText = cursor.GetChild(0).GetComponent<Text>();
            
        }

        cells = new CellScript[invCellPan.childCount];
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = invCellPan.GetChild(i).GetComponent<CellScript>().GetLinkSetSettings(i, this);
            
        }
        eqCells = new EquipCell[3];
        for (int i = 0; i < eqCells.Length; i++)
        {
            eqCells[i] = equipPan.GetChild(i).GetComponent<EquipCell>();
            
            
        }
    }

    #region Drags
    public void OnDrag(PointerEventData eventData)
    {
        cursor.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!cursorCell) return;
        if (!Inventory.instanceI.items[cursorCell.cellID]) return;
        
        prevCell = cursorCell;
        selectedCell = cursorCell;
        
        RefreshAll();
        cursor.gameObject.SetActive(true);
        cursorImage.sprite = Inventory.instanceI.items[prevCell.cellID].mySpr;
        cursorText.text = Inventory.instanceI.count[prevCell.cellID].ToString();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (cursorCell == prevCell)
        {
            ClearCursor();
            return;
        }
        if (cursorCell == null && prevCell)
        {
            Inventory.instanceI.DropItem(prevCell);
            ClearCursor();
        }
        if (cursorCell && prevCell)
        {
            
            if (cursorCell.isFree)Inventory.instanceI.MoveItem(prevCell.cellID, cursorCell.cellID);
            else Inventory.instanceI.SwapItems(prevCell.cellID, cursorCell.cellID);
            ClearCursor();

        }
    }
    #endregion


    private void Update()
    {
        if (cursorCell)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (Inventory.instanceI.Use(cursorCell.cellID)) RefreshAll();
            }
        }
    }
    private void ClearCursor()
    {
        cursor.gameObject.SetActive(false);
        prevCell = null;
        selectedCell = cursorCell;
        
        RefreshAll();
    }

    public void CursorCellSwitch(CellScript newCell)
    {
        if (!cursorCell)
        {

            cursorCell = newCell;
        }
        else
        {
            cursorCell = null;
        }

        RefreshAll();
    }

    public void RefreshAll()
    {
        for (int  i = 0;  i < cells.Length; i++)
        {
            cells[i].Equiped = false;
            if (Inventory.instanceI.items[cells[i].cellID] != null)
            {
                int ind = (int)Inventory.instanceI.items[i].type;
                if (Inventory.instanceI.equipment.Length > ind)
                {
                    if (Inventory.instanceI.equipment[ind] == Inventory.instanceI.items[i]) cells[i].Equiped = true;
                }
            }
            cells[i].Refresh();
            
        }
        
        for (int i = 0; i < eqCells.Length; i++)
        {
            eqCells[i].Refresh();
            
        }
    }

    public void Cleaner()
    {
        ClearCursor();
        cursorCell = null;
        selectedCell = null;
        RefreshAll();
    }
}
