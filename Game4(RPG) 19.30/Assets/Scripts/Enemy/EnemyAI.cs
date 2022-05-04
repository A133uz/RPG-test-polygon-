using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyStats))]
public class EnemyAI : MonoBehaviour
{
    public enum EnemyType 
    {
        simp = 0, shoot = 1, bigBob = 2
    }
    public EnemyType Etype = EnemyType.simp;
    #region Headers
    [Header("Move Setts")]
    public float sp; //обычная скорость 
    public float frceSp;
    public float frceCountd;
    public float patrolSp; //скорость при свободном перемещении

    [Header("Radius Setts")]
    [SerializeField] protected float _chasingRadius;
    [SerializeField] protected float _attackRadius;
    [SerializeField] protected float _retreatRadius;
    [SerializeField] protected float _walkDist;

    [Header("Setts For Shooter")]
    [SerializeField] protected GameObject bulletPref;
    [SerializeField] protected float _fireRate;

    [Header("Other Setts")]
    [SerializeField] protected bool _moveRight = false;
    [SerializeField] protected bool _moveStop = false;
    [SerializeField] protected bool _CanMove = true;
    protected Transform _target;
    protected Rigidbody2D _rb2D;
    protected SpriteRenderer _mySprite;
    protected bool _facingRight;
    protected bool _isFrced;
    protected float _mySp; //настоящая скорость врага (04.04.22 - скорость преследования)
    #endregion

    protected Vector3 _startPos, _movePos;

    protected bool _gameStarted;

    protected EnemyStats myStats;

    protected void Start()
    {
        _gameStarted = true;
        _startPos = transform.position;
        _rb2D = GetComponent<Rigidbody2D>();
        myStats = GetComponent<EnemyStats>();
        _mySprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        _target = FindObjectOfType<PlayerCont>().transform;
        _mySp = sp;

        _movePos = GeneratePoint();
    }

    

    protected void AICheck()
    {
        switch (Etype)
        {
            case EnemyType.simp: Searchin(); break;
            case EnemyType.shoot: break;
        }

    }



    protected void Shooter()
    {
        


    }
    protected void Searchin()
    {
        float dist = Vector3.Distance(transform.position, _target.position);
        if ( dist <= _chasingRadius)
        {
            Chasin();
        }
        else Patrol();
    }

    protected void Chasin()
    {
        MoveManage(_mySp, _target.position);
        if (!_isFrced) StartCoroutine(Force());
    }

    protected void Patrol()
    {
        MoveManage(patrolSp, _movePos);
        if(Vector3.Distance(transform.position, _movePos) < 0.1f) _movePos = GeneratePoint(); 

    }

    void MoveManage(float sp, Vector3 pos)
    {
        Vector2 tmp = Vector2.MoveTowards(transform.position, pos, sp * Time.deltaTime);
        _rb2D.MovePosition(tmp);
    }

    Vector3 GeneratePoint()
    {
       return   new Vector2(_startPos.x + Random.Range(-_walkDist, _walkDist), _startPos.y + Random.Range(-_walkDist, _walkDist));
    }

    protected IEnumerator Force()
    {
        _isFrced = true;
        _mySp = 0.5f;
        yield return new WaitForSeconds(0.5f);
        _mySp = frceSp;
        yield return new WaitForSeconds(0.2f);
        _rb2D.AddForce(transform.forward * _mySp, ForceMode2D.Impulse);
        _mySp = sp;
        yield return new WaitForSeconds(frceCountd);
        _isFrced = false;
    }

  
    



    protected void OnDrawGizmosSelected()
    {
        if (!_gameStarted) _startPos = transform.position;
        Gizmos.color = Color.green;

        float tmpMOD = 0;

        Vector3 pos = new Vector2(_startPos.x + tmpMOD, _startPos.y + tmpMOD);
        Vector3 size = new Vector2(_walkDist * 2, _walkDist * 2);

        Gizmos.DrawWireCube(pos, size);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chasingRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_movePos, 0.2f);
    }

    
}
