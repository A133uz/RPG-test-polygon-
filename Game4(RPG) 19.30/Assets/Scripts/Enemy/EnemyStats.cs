using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Stats")]
    public int hp = 50;
     public int dmg = 5;
    public int hitPulse = 30;

    [Space]
    [SerializeField] private int _exp = 10;

    public Garant[] garantItem;
    public Chance[] chnceItem;

    public GameObject drop;
    

    private void Start()
    {
        drop = Resources.Load<GameObject>("Prefabs/Someitem");

    }

    private void Update()
    {
        if (hp <= 0)
        {
            PlayerStats.expPts += _exp;
            Die();
        }
    }

    void Die()
    {
        for (int i = 0; i < garantItem.Length; i++)
        {
            ItemSett tempDrop = Instantiate(drop, Random.insideUnitSphere * 1.5f + transform.position, Quaternion.identity).GetComponent<ItemSett>();
            tempDrop.item = garantItem[i].item;
            tempDrop.count = garantItem[i].count;
        }

        for (int i = 0; i < chnceItem.Length; i++)
        {
            int rnd = Random.Range(1, 101);
            if (rnd <= chnceItem[i].chance)
            {
                ItemSett tempDrop = Instantiate(drop, Random.insideUnitSphere * 1.5f + transform.position, Quaternion.identity).GetComponent<ItemSett>();
                tempDrop.item = chnceItem[i].item;
                tempDrop.count = chnceItem[i].count;
            }
            
        }
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        Destroy(gameObject, 0.5f);
        this.enabled = false;
    }
}

[System.Serializable]
public struct Garant
{
    public Item item;
    public int count;
}

[System.Serializable]
public struct Chance
{
    public Item item;
    public int count;
    public float chance;
}