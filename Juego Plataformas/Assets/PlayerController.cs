using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 5f;

    public float speed = 2f;
    private Rigidbody2D rb2d;
    private Animator anim;
    public bool grounded;

    public float jumpPower = 6.5f;
    private bool jump;
    private bool doublejump;
    private bool  movement = true;

    private SpriteRenderer spr;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);

        if (grounded)
        {
            doublejump = true;
        }

        if (Input.GetKeyDown("w") )
        {
            if (grounded)
            {
                jump = true;
                doublejump = true;
            }
            else if (doublejump)
            {
                jump = true;
                doublejump = false;
            }
        }
         
	}

     void FixedUpdate()
    {

        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.75f; 

        if (grounded)
        {
            rb2d.velocity = fixedVelocity; //frenar velocidad cuando no existe friccion/ es una friccion manual
        }

        float h = Input.GetAxis("Horizontal");
        if (!movement) h = 0;
        rb2d.AddForce(Vector2.right*speed*h); // fuerza aplicada que gener velocidad al player

        /*if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
           rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }*/

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed); //limitar la aceleracion de la velocidad de player
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);


        if(h > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if(h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // hacerlo flipear cuando corra a la izquierda
        }

        if (jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0); //cancelamos eje y de la velocidad para no sumar impulsos
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //fuerza aplicada para que salte
            jump = false;
        }
        //Debug.Log(rb2d.velocity.x);
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector3(-1, 0, 0); //respawn
    }

    public void EnemyJump()
    {
        jump = true;
    }

    public void EnemyKnockBack(float enemyPosX)
    {
        jump = true;

        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);
        movement = false;
        Invoke("EnableMovement", 0.7f);

        Color color = new Color(255/255f, 106/255f, 0/255f);
        spr.color = color;
    }

    void EnableMovement()
    {
        movement = true;
        spr.color = Color.white;
    }
}
