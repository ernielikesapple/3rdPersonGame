using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    enum AttackType
    {
        Melee,
        Range
    };
    public GameObject questWindow;

    public Quest quest;

    public Animator animator;

    public Transform attackPoint; // attach on the sword

    public float meleeAttackRange = 0.5f;
    public float rangeAttackRange = 0.5f;
    public int attackDamage = 40;

    public LayerMask enemyLayers;

    #region singleton

    public static Player instance;

    public Text questCompeltedNotice;

    public Text currentQuestNumber;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // play an attack animation
        if (Input.GetButtonDown("Fire1"))   // Melee Attack
        {
            Attack(AttackType.Melee);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("attacking", false);
        }

        if (Input.GetButtonDown("Fire2"))  // range Attack
        {
            Attack(AttackType.Range);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            animator.SetBool("rangeAttack", false);
        }


        if (quest.isActive && quest.goal.IsReached())
        {

            quest.Complete();
            
            currentQuestNumber.text = "current quest number:  0";
            

            StartCoroutine(ActivationRoutine());
        }

    }



    void Attack(AttackType attackType)
    {

        Collider[] hitEnemies;  // Detect enmies in range of attack

        if (attackType == AttackType.Melee)
        {
            animator.SetBool("attacking", true);
            hitEnemies = Physics.OverlapSphere(attackPoint.position, meleeAttackRange, enemyLayers);

            // damage them
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<GoblinController>().TakeDamage(attackDamage);
            }
            Debug.Log("Melee HIT ");
        }
        else if (attackType == AttackType.Range)
        {
            animator.SetBool("rangeAttack", true);
            hitEnemies = Physics.OverlapSphere(attackPoint.position, meleeAttackRange, enemyLayers);
            // damage them
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<GoblinController>().TakeDamage(attackDamage);
            }
            Debug.Log("range HIT ");
        }

    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, rangeAttackRange);

        Gizmos.DrawWireSphere(attackPoint.position, meleeAttackRange);
    }



    private IEnumerator ActivationRoutine()
    {
        //Wait for 14 secs.
        //yield return new WaitForSeconds(14);

        //Turn My game object that is set to false(off) to True(on).
        questCompeltedNotice.gameObject.SetActive(true);

            

        //Turn the Game Oject back off after 1 sec.
        yield return new WaitForSeconds(5);
        Debug.Log("should be active");
        //Game object will turn off
        questCompeltedNotice.gameObject.SetActive(false);
    }

}

