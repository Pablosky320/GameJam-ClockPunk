using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] int currentHealth;
    public Animator animator;
    bool isInvincible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {

            currentHealth = currentHealth - damage;
            StartCoroutine(InvincibilityPeriod());

        Debug.Log("el enemigo recibe daño");



        if(currentHealth <= 0)
        {
            Die();
        }
    }
    
    IEnumerator InvincibilityPeriod()
    {
        isInvincible = true;
        yield return new WaitForSeconds(1);
    }

    void Die()
    {
        Debug.Log("el enemigo esta muerto");
        Destroy(gameObject);
    }
}
