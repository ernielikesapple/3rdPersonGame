using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class GoblinController : MonoBehaviour
{

    public float lookRadius = 10f;

    Transform target;
    public Player refToPlayer;
    NavMeshAgent agent;

    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;

    public CharacterController gbController;
    
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;
    public float gravity = -9.81f;

    public Animator animator;

    void Start()
    {
        target =Player.instance.player.transform;
        refToPlayer = Player.instance;
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -15f;
        }
        velocity.y += gravity * 1.8f * Time.deltaTime;
        gbController.Move(velocity * Time.deltaTime);


        float x = Random.Range(0, 3);
        float z = Random.Range(-3, 3);

        if (x > 0)
        {
            transform.position += transform.forward * x * Time.deltaTime;
            transform.eulerAngles += new Vector3(0.0f, 50.0f, 0.0f) * z * Time.deltaTime;
            x -= 1;
            animator.SetBool("randomMoving", true);
        }


        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {

            animator.SetBool("randomMoving", false);

            agent.SetDestination(target.position);

            transform.Translate(Vector3.forward * Time.deltaTime);
            animator.SetBool("moving", true);


            if(distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }

        }
        else
        {
            animator.SetBool("moving", false);
        }
        if (distance > agent.stoppingDistance)
        {
            animator.SetBool("attacking", false);
        }

    }
    void FaceTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
       
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            animator.SetBool("attacking", true);
            animator.SetBool("moving", false);
        }
     
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("hurt");
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {

            Die();

        }
    }

    void Die()
    {

        Debug.Log("Enemy Died!");

        animator.SetBool("isDead", true);

        refToPlayer.quest.goal.EnemyKilled();

        GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject,2);
        this.enabled = false;
    }

}
