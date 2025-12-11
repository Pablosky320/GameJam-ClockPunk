using System.Collections;
using UnityEngine;



public class Attack : MonoBehaviour
{
    public LayerMask enemyLayers;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public Enemy enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attacking();
        }
    }

    void Attacking()
    {
        animator.SetTrigger("Attack");
        
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        
        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(20);
        }
    }


}

