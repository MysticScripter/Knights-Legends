using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Rigidbody2D rb;
    public float startHealth;
    public float health;
    public Animator animator;
    public Image healthbr;
    private bool played;

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<Score>().GetComponent<Score>().UpdateScore(startHealth);
        Destroy(gameObject);
    }

    private void Start()
    {
        health = startHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (gameObject.tag == "Dead" && !played)
        {
            FindObjectOfType<AudioManager>().Play("ZombieDeath" + (object)Random.Range(1, 3));
            played = true;
        }
    }

    private void Update()
    {
        healthbr.fillAmount = health / startHealth;
        if (health <= 0.0)
        {
            gameObject.tag = "Dead";
            animator.SetBool("isDead", true);
            Destroy(GetComponent<Collider2D>());
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            StartCoroutine("Wait");
        }
        
    }
}
