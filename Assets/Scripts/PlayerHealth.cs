using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float _hitTime = 2f;
    private bool _canHit = true;
    public float startHealth;
    public float health;
    public Animator animator;
    private EnemyHealth enhealth;
    public Image healthbr;
    private float _hitTimer;

    private void Die()
    {
        animator.SetBool("isDead", true);
        Destroy(gameObject, 8f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!_canHit)
            return;
        if (col.collider.tag == "Zombie" && col.collider is CircleCollider2D && col.otherCollider is PolygonCollider2D && enhealth.health > 0.0)
            --health;
        _hitTimer = 0.0f;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = startHealth;
    }

    private void Update()
    {
        _hitTimer += Time.deltaTime;
        if (_hitTimer > _hitTime)
            _canHit = true;
        healthbr.fillAmount = health / startHealth;
        try
        {
            enhealth = GameObject.FindWithTag("Zombie").GetComponent<EnemyHealth>();
        }
        catch
        {
            return;
        }
        if (health <= 0.0)
            Die();

    }
}
