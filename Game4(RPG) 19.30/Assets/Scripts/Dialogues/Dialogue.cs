using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Dialogue", menuName ="Dialogue", order = 3)]
public class Dialogue : ScriptableObject
{
    [TextArea(2, 5)]
    public string npcName;
    public Sprite npcImage;

    public Replics[] reps;
}

[System.Serializable]
public struct Replics
{
    [TextArea(2, 5)]
    public string[] txt;

    public string[] answers;
    public int[] links;
}