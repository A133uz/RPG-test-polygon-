using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public ItemSett item;

    public static Interact instance;
    public Inventory inv;
    private void Awake()
    {
        instance = this;
        inv = GetComponent<Inventory>();
    }
    private void Update()
    {
        if (item != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //sound

                TakeItem();
            }
        }
    }
    void TakeItem()
    {
        if (inv.AddItem(item.item, item.count))
        {
            print("You take " + item.item.Name);
            Destroy(item.gameObject);
        }
        else
        {
            print("Inventory is full");
        }
    }
}
