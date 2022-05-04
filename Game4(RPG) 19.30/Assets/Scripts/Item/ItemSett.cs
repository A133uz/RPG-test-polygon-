using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSett : MonoBehaviour
{
    public Item item;
    public int count = 1;
    private void Start()
    {
        if (item)
        {
            GetComponent<SpriteRenderer>().sprite = item.mySpr;
            gameObject.name = item.Name;
        }
        else Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact.instance.item = this;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact.instance.item = null;
        }
    }
}
