using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDmg : MonoBehaviour
{
   private Rigidbody2D _rb;
   PlayerCont _pc;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _pc = GetComponent<PlayerCont>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            EnemyStats es = collision.transform.GetComponent<EnemyStats>();
            Pulse(collision.transform, es.hitPulse);
            Damage(es.dmg);
        }
    }
    private void Pulse(Transform dir, float pulse)
    {
        _rb.velocity = Vector2.zero;
        if (dir.position.x > transform.position.x)
        {
            _rb.AddForce(Vector2.left * pulse, ForceMode2D.Impulse);
        }
        else _rb.AddForce(Vector2.right * pulse, ForceMode2D.Impulse);
    }

    private void Damage(int dmg)
    {
        int dm = dmg * PlayerStats.protection / 2;
        if (dm <= 0) dmg = 0;
        PlayerStats.hp -= dm;
    }

   
}
