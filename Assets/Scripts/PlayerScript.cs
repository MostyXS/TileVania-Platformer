using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    bool isAlive = true;
    [SerializeField] float movementSpeed=1f;
    [SerializeField] float jumpHeight = 5f;
    float currentLocalScaleX;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    [SerializeField] float climbSpeed=5f;
    float kickFloat = 100f;
    [SerializeField] Slider HealthBar;
    [SerializeField] float health = 100;
    float maxHealth;
    
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        myRigidBody = GetComponent<Rigidbody2D>();
        currentLocalScaleX = transform.localScale.x;
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    
    void Update()
    {
        
        
        if (isAlive)
        {
            GetDamage();
            Move();
            Climb();
            Jump();
            TrapCheck();
        }

    }

    private void TrapCheck()
    {
        if (myRigidBody.IsTouchingLayers(LayerMask.GetMask("Traps")))
            DeathWithAnim();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlive)
        {
        if (collision.GetComponent<Enemy>())
            DeathWithAnim();
        }
    }
   
    

    private void Move()
    {
        
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        transform.position = new Vector2(transform.position.x + deltaX, transform.position.y);
        if (Input.GetAxis("Horizontal") != 0)
            GetComponent<Animator>().SetBool("Running", true);
        else
                GetComponent<Animator>().SetBool("Running", false);

        if (Input.GetAxis("Horizontal") < 0)
            currentLocalScaleX = -1;
        else if (Input.GetAxis("Horizontal") > 0)
            currentLocalScaleX = 1;

        transform.localScale = new Vector2(currentLocalScaleX, transform.localScale.y);
        



    }
    private void Jump()
    {
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpHeight);
                myRigidBody.velocity += jumpVelocityToAdd;
            }
        }
    }

    private void Climb()
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        { myRigidBody.gravityScale = 1; GetComponent<Animator>().SetBool("Climbing", false); return; }
        
        myRigidBody.gravityScale = 0;
            float controlThrow = Input.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, climbSpeed * controlThrow);
            myRigidBody.velocity = climbVelocity;
        if (Input.GetButtonDown("Vertical"))
        {
            GetComponent<Animator>().SetBool("Climbing", true);
        }
        if (Input.GetButtonUp("Vertical"))
        {
            GetComponent<Animator>().SetBool("Climbing", true);
        }

       


    }
    private void DeathWithAnim()
    {
        GetComponent<Animator>().SetBool("Dead",true);
        myRigidBody.velocity = new Vector2(0, kickFloat);
        isAlive = false;
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
        
            
    }
    private void GetDamage()
    {
        if (Input.GetButton("Fire1"))
        {
            health -= 20;
        }
        if (!HealthBar) { return; }
        HealthBar.value = health / maxHealth;
    }
}
