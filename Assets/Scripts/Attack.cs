using System.Collections;
using UnityEngine;



public class Attack : MonoBehaviour
{
    public LayerMask enemyLayers;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 1f;
    public Enemy enemy;
    public int damageDealt = 20;

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
        Debug.Log("El personaje ataca");
        
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        
        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damageDealt);
        }
    }

    void OnDrawGizmos() 
    {
        if (attackPoint == null)
        {
            return;
        }    
        Gizmos.DrawSphere(attackPoint.position, 1);
    }
}

