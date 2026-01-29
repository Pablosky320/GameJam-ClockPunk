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
    public GameObject attackHitbox;
    public float attackCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("El personaje ataca");
            StartCoroutine(Attacking());
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

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(2);
    }
}

