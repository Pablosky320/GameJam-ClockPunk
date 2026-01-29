using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] int currentHealth;
    public Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("el enemigo recibe daño");

        currentHealth = currentHealth - damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("el enemigo esta muerto");
        Destroy(gameObject);
    }
}
