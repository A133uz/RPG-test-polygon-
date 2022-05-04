using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDie : MonoBehaviour
{
    private Rigidbody2D _rb;
    private EnemyStats _es;
    private EnemyAI _eAI;

    //Color
    private bool isHit;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _es = GetComponent<EnemyStats>();
        _eAI = GetComponent<EnemyAI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit(collision.transform); 
    }

    private void Hit(Transform trfr)
    {
        if (trfr.tag == "Sword")
        {
            if (!isHit)
            {
                _es.hp -= PlayerStats.MelDmg + 20;
                _rb.velocity = new Vector2(0, 0);
                Transform tmpTr = trfr.parent.parent.transform;
                if (tmpTr.position.x > transform.position.x)
                {
                    _rb.AddForce(Vector2.left * Inventory.instanceI.equipment[0].pulse);
                }
                else _rb.AddForce(Vector2.right * Inventory.instanceI.equipment[0].pulse);
            }
        }
        
    }
}
