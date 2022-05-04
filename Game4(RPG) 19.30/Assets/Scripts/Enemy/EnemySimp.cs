using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimp : EnemyAI
{
    private void FixedUpdate()
    {
        if (_CanMove) AICheck();
    }
}
