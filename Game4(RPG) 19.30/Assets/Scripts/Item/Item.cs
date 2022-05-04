using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        melee = 0,
        distance = 1,
        armor = 2,
        item = 3,
        gold = 4
    }

    public ItemType type = ItemType.gold;
    public int id;
    public Sprite mySpr;
    public string Name, desc;

    public bool isUseful, isArrow;
    public int ArrowID;

    [Header("Weapon Settings")]
    public int dmg;
    public float pulse, speed, weight;
    [Space]
    public float length, offset;
    [Space]
    public Sprite spriteForBowStage;
    public GameObject myArrow;

    [Header("Armor Settings")]
    public int protect;

    
}
