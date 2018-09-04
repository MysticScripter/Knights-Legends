using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    private float checkRadius = 0.3f;
    public Animator animator;
    public Rigidbody2D knight;
    public Rigidbody2D zombie;
    private bool facingRight;
    private bool playerContact;
    public LayerMask whatIsPlayer;
    public Transform playerCheck;
    public float minDis;
    private EnemyHealth enhealth;

    private void Start()
    {
        knight = GameObject.Find("Knight").GetComponent<Rigidbody2D>();
        enhealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        zombie = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    private void Update()
    {
        if (enhealth.health > 0.0 && knight != null)
        {
            animator.SetFloat("speed", Mathf.Abs(zombie.velocity.x));
            playerContact = Physics2D.OverlapCircle(playerCheck.position, checkRadius, whatIsPlayer);
            animator.SetBool("Attack", playerContact);
            if (Mathf.Sqrt(Mathf.Pow(knight.position.x - zombie.position.x, 2f)) > minDis)
            {
                if (knight.position.x > zombie.position.x)
                {
                    if (!facingRight)
                        Flip();
                    zombie.velocity = new Vector2(speed, zombie.velocity.y);
                }
                if (knight.position.x >= zombie.position.x)
                    return;
                if (facingRight)
                    Flip();
                zombie.velocity = new Vector2(-1f * speed, zombie.velocity.y);
            }
            else
                zombie.velocity = new Vector2(0.0f, zombie.velocity.y);
        }
        else
        {
            if (knight == null)
                return;
            zombie.velocity = new Vector2(0.0f, 0.0f);
            animator.SetFloat("speed", 0.0f);
            animator.SetBool("Attack", false);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
