using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Interact))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerCont : MonoBehaviour
{
    [Header("Player Setts")]
    public float sp;
    public float dashF;
    public float staminaLose;
    public float dashCountDown;
    private float dashTimer;
    

    private Transform mySprite;
    private Vector3 Axis, AxisPlus, Cursor;
    private bool isStand, isFlip, isDashed;


    [Header("Weapon")]
    private float bowReady;
    public bool arrowReady;
    private bool bowCharged, isMelee;

    public Transform arrowPoint;
    public GameObject mySwrd, myBow;
    private SpriteRenderer swrdRndr, bowRndr;
    private Animator swrdAnim;
    private BoxCollider2D swrdCol;
    private Rigidbody2D rb2D;
    public static PlayerCont instance;

    private void Awake()
    {
        mySprite = transform.GetChild(0);
        rb2D = GetComponent<Rigidbody2D>();
        instance = this;

        mySwrd = mySprite.GetChild(0).gameObject;
        swrdCol = mySwrd.GetComponent<BoxCollider2D>();
        swrdRndr = mySwrd.GetComponent<SpriteRenderer>();
        swrdAnim = mySwrd.GetComponent<Animator>();
        mySwrd.SetActive(false);

        myBow = mySprite.GetChild(1).gameObject;
        bowRndr = myBow.transform.GetChild(0).GetComponent<SpriteRenderer>();
        arrowPoint = myBow.transform.GetChild(1).GetComponent<Transform>();
        myBow.SetActive(false);
    }

    private void Update()
    {
        Axis = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        AxisPlus = new Vector3(Input.GetAxis("Horizontal+"), Input.GetAxis("Vertical+"));
        Cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        isStand = Axis.magnitude == 0;

        Dash();
        Attack();
    }

    private void FixedUpdate()
    {
       if(!isDashed) Move();
         
    }
    #region Move
    private void Move()
    {
        rb2D.velocity = Axis * sp * Time.deltaTime;
        mySprite.eulerAngles = new Vector3(0,0, 15 * -Axis.x);
        if (mySprite.position.x > Cursor.x && !isFlip)
        {
            Flip();
        }
        if (mySprite.position.x < Cursor.x && isFlip)
        {

            Flip();
        }
    }
    void Flip()
    {
        isFlip = !isFlip;
        transform.localScale *= new Vector2(-1, 1);
    }
    void Dash()
    {
        if (!isDashed && !isStand && Input.GetKeyDown(KeyCode.Space))
        {
            isDashed = true;
            dashTimer = dashCountDown;
            rb2D.AddForce(dashF * AxisPlus, ForceMode2D.Impulse); 
        }


        if (dashTimer > 0) dashTimer -= Time.deltaTime;
        else if (isDashed)
        {
            dashTimer = 0;
            isDashed = false;
            rb2D.velocity = Vector3.zero;
        }
    }
    #endregion
    private void Attack()
    {
        if (Input.GetMouseButton(1)) Distance();
        else Melee();

        HitOff();
    }
    private void Melee()
    {
        if (Input.GetMouseButtonDown(0) && !isMelee)
        {
            if (Inventory.instanceI.equipment[0] != null)
            {
                swrdRndr.sprite = Inventory.instanceI.equipment[0].mySpr;
                swrdCol.size = new Vector2(0.4f, Inventory.instanceI.equipment[0].length);
                swrdCol.offset = new Vector2(0, Inventory.instanceI.equipment[0].offset);

                float sp = Inventory.instanceI.equipment[0].speed; // + от ловкости
                swrdAnim.speed = sp;
                isMelee = true;
                mySwrd.SetActive(true);
            }
        }
    }

    private void Distance()
    {
        if (!Inventory.instanceI.equipment[1] || isMelee) return;

        myBow.SetActive(true);
        Vector2 bowPos = myBow.transform.position;
        Vector2 direct = (Vector2)Cursor - bowPos;
        
        myBow.transform.right = direct;
        
        //stop to heal stamina

        bowReady += Inventory.instanceI.equipment[1].speed * 0.5f * Time.deltaTime; //+ от ловкости

        if (bowReady <= 2)
        {
            if (bowRndr.sprite != Inventory.instanceI.equipment[1].mySpr) bowRndr.sprite = Inventory.instanceI.equipment[1].mySpr;
            if (bowReady < 1f) arrowPoint.gameObject.SetActive(false);
            else
            {
                if (Inventory.instanceI.ArrowCheck(Inventory.instanceI.equipment[1].ArrowID))
                {
                    arrowReady = true;
                    arrowPoint.gameObject.SetActive(true);
                    arrowPoint.localPosition = new Vector3(1.25f, 0, 0);
                    arrowPoint.GetComponent<SpriteRenderer>().sprite = Inventory.instanceI.items[Inventory.instanceI.arrowID].mySpr;
                }
                else arrowPoint.gameObject.SetActive(false);
            }

        }
        else if (arrowReady)
        {
            if (bowRndr.sprite != Inventory.instanceI.equipment[1].spriteForBowStage) bowRndr.sprite = Inventory.instanceI.equipment[1].spriteForBowStage;
            arrowPoint.localPosition = new Vector3(0.65f, 0, 0);
            bowCharged = true;
        }

        if (bowCharged)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                bowCharged = false;
                arrowReady = false;
                bowReady = 0;
                Instantiate(Inventory.instanceI.equipment[1].myArrow, arrowPoint.position, arrowPoint.rotation);
                Inventory.instanceI.ArrowUse();
            }
        }
    }
    private void HitOff()
    {
        if (isMelee)
        {
            if (!swrdAnim.GetCurrentAnimatorStateInfo(0).IsName("SwrdHit"))
            {
                isMelee = false;
                mySwrd.SetActive(false);
            }
        }
    }
}
