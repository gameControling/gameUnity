using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class move_player : SoundsScript
{
    public float maxSpeed = 10f, curSpeed = 0f, poverSpeed = 0.25f, bufSpeed = 2;
    public int damage = 20;
    float maxHealth = 100f;
    public float health;
    public float attackRate = 2f;
    public float attackRange = 5f;
    public float deshPover = 75f;
    float jumpForce = 30f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Bullet bullet;
    [SerializeField] Blade blade;
    InputAction move, jump, attack, sprint, fire;
    [SerializeField] SpriteRenderer renderer, renderCanvas;
    [SerializeField] Transform CheckWallPoint;
    ControlAnimation controlAnimation;
    [SerializeField] Vector3 groundChechOffset;
    [SerializeField] LayerMask ground;
    [SerializeField] GameObject HPbar;
    SavePointLogic savePointLogic;

    float timerTakeDamage = 2f;
    float timerFire = 0;
    float timerAttack = 0;
    float timerDesh = 0;
    float timerBlockMove = 0;
    float timerColor = 0;

    bool isGround = false;
    bool onWall = false;

    private void Start()
    {
        health = maxHealth;
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        sprint = InputSystem.actions.FindAction("Sprint");
        fire = InputSystem.actions.FindAction("Fire");
        attack = InputSystem.actions.FindAction("Attack");
        controlAnimation = GetComponentInChildren<ControlAnimation>();

        savePointLogic = new SavePointLogic();
        if (savePointLogic.getOpenFile())
        {
            maxSpeed = savePointLogic.getSpeed();
            damage = savePointLogic.getDmg();
            health = savePointLogic.getHp();
            attackRange = savePointLogic.getAttackRange();
            transform.position = new Vector3(savePointLogic.getPositionX(), savePointLogic.getPositionY());
            deshPover = savePointLogic.getDeshPower();
        }
    }

    private void FixedUpdate()
    {
        if (timerColor != 0) 
        {
            timerColor -= Time.deltaTime;
        }
        else
        {
            renderCanvas.color = Color.white;
        }
        if (timerBlockMove > 0)
        {
            timerBlockMove -= Time.deltaTime;
        }
        else
        {
            Move();
        }   
        checingWall();
        
        if (timerTakeDamage > 0)
        {
            timerTakeDamage -= Time.deltaTime;
        }
        if(jump.triggered)   
        { 
            if (isGround)
            {
                Jump();
                      
            }
            else if (!isGround && onWall)
            {
                WallJump();
            }
        }
        if (timerFire > 0)
        {
            timerFire -= Time.deltaTime;
        }
        else
        {
            if (fire.triggered)
            {
                Fire();
                timerFire = 2f;
            }
        }
        if (timerAttack > 0)
        {
            timerAttack -= Time.deltaTime;
        }
        else
        {
            if (attack.triggered)
            {
                Attack();
                timerAttack = 2f;
            }
        }
        if (timerDesh > 0)
        {
            timerDesh -= Time.deltaTime;
        }
        else
        {
            if (sprint.triggered)
            {
                Desh();
                timerDesh = 2f;
            }
        }
        controlAnimation.isFly = IsFlying();

    }

    private void Move()
    {
        Vector2 dir = move.ReadValue<Vector2>();
        Vector3 newPos = new Vector3(dir.x, dir.y);
        
        if (newPos.x + transform.position.x > transform.position.x)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            renderer.flipX = false;
            controlAnimation.isMove = true;
        }
        else if(newPos.x + transform.position.x < transform.position.x)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            renderer.flipX = true;
            controlAnimation.isMove = true;
        }
        else
        {
            controlAnimation.isMove = false;
        }
        if (controlAnimation.isMove && rb.linearVelocity.y == 0)
        {
            PlaySound(1, 0.5f, random: true);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) 
        {
            if (curSpeed <= maxSpeed)
            {
                curSpeed += poverSpeed;
                poverSpeed *= bufSpeed;
            }
        }
        else
        {
            if(rb.linearVelocityX > 0)
            {
                curSpeed = -0.5f;
            }
            else
            {
                curSpeed = 0;
                poverSpeed = 0.125f;
            }
        }

        rb.AddForce(newPos * (curSpeed * 3000) * Time.deltaTime);
    }

    private void Desh()
    {
        PlaySound(1);
        Vector2 dir = move.ReadValue<Vector2>();
        Vector3 newPos = new Vector3(dir.x, dir.y);
        transform.position += newPos * deshPover * Time.deltaTime;
    }

    private void Jump()
    {
        controlAnimation.Jump();

        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        PlaySound(2, 0.5f);
    }

    private void Attack()
    {
        PlaySound(0, random: true);
        Vector3 posBlade = renderer.flipX == true ? new Vector2(-1, 0) : new Vector2(1, 0);
        Instantiate(blade, transform.position, transform.rotation);
    }

    private void Fire()
    {
        PlaySound(0);
        controlAnimation.Fire();
        Vector3 posBullet = renderer.flipX == true ? new Vector2(-1, 0) : new Vector2(1, 0);
        Instantiate(bullet, transform.position + posBullet, transform.rotation);
    }

    public void TakeDamage(float takeDmg)
    {
        if(timerTakeDamage < 0)
        {
            Debug.Log("takeDamage");
            health -= takeDmg;

            HPbar.GetComponent<Image>().fillAmount = health / 100;

            if (health <= 0)
            {
                Die();
            }
            timerTakeDamage = 2f;
        }  
    }

    public void Die()
    {
        renderCanvas.color = Color.black;
        transform.position = new Vector3(savePointLogic.getPositionX(), savePointLogic.getPositionY());
        HPbar.GetComponent<Image>().fillAmount = maxHealth;
    }

    public void Hill()
    {
        health = maxHealth;
        HPbar.GetComponent<Image>().fillAmount = maxHealth;
    }

    public bool retDerOfMove()
    {
        return !renderer.flipX;
    }

    private bool IsFlying()
    {
        if (rb.linearVelocityY < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Vall")
        {
            isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Vall")
        {
            isGround = false;
        }
    }

    void checingWall()
    {
        if (renderer.flipX)
        {
            onWall = Physics2D.OverlapCircle(CheckWallPoint.position, 0.5f, ground);
        }
        else
        {
            onWall = Physics2D.OverlapCircle(CheckWallPoint.position + new Vector3(1, 0), 0.5f, ground);
        }
    }

    void WallJump()
    {
        PlaySound(2, 0.5f);
        rb.linearVelocity = renderer.flipX ? new Vector2(maxSpeed * 3, jumpForce) : new Vector2(maxSpeed * -3, jumpForce);
        timerBlockMove = 0.5f;
    }
}