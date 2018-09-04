using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    private bool facingRight = true;
    public float jumpForce = 7f;
    public float groundRadius = 0.2f;
    public Animator animator;
    private bool grounded;
    private bool attackPressed;
    public Transform groundCheck;
    public Transform pointA;
    public Transform pointB;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsGround;
    private bool attackhit;
    private PlayerHealth playerhealth;
    private Rigidbody2D player;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerhealth = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerhealth.health >= 0.0)
        {
            Jump();
            Attack();
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Knight_attack") && !attackhit)
                CheckAttack();
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Knight_attack"))
                attackhit = false;
        }
    }

    private void FixedUpdate()
    {
        if(playerhealth.health > 0)
        {
            Move();
        }
    }

    private void Jump()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius,whatIsGround);
        animator.SetBool("Ground", grounded);
        bool buttonDown = Input.GetButtonDown("Jump");
        animator.SetBool("JumpPressed", buttonDown);
        if (grounded && buttonDown)
        {
            animator.SetBool("Ground", false);
            player.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("JumpPressed", buttonDown);
            FindObjectOfType<AudioManager>().Play("Jump");
        }
        
    }

    private void Attack()
    {
        attackPressed = Input.GetButtonDown("Fire1");
        animator.SetBool("AttackPressed", attackPressed);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Knight_attack"))
            return;
        
        if (attackPressed)
            FindObjectOfType<AudioManager>().Play("SwordSwing");
    }

    private void CheckAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapAreaAll(pointA.position, pointB.position, whatIsEnemy);
        {
            foreach(Collider2D enemy in enemies)
            {
                enemy.GetComponent<EnemyHealth>().health--;
                attackhit = true;
            }
        }
        
    }

    private void Move()
    {
        float move = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(move * speed, player.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(move));
        if (move > 0.0 && !facingRight)
            Flip();
        if (move < 0.0 && facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
