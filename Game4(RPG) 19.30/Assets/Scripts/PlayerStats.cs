using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats stats;

    public static int hp, maxHP, mana, maxMana, stamina, maxStmn;
    public static int strength, agillity, constitusion;
    public static int level = 1;
    public static int exp, expPts;
    public static int protection;
    public static int MelDmg;
    public static int DisDmg;
    public static int MagicDmg;


    public static string PName;
    public static Sprite PSpr;
    Image hpBar, manaBar, expBar, staBar;
    Text hpTxt, manaTxt, expTxt, staTxt;

    
}
