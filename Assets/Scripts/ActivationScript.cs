using UnityEngine;

public class ActivationScript : MonoBehaviour
{
    public EnemyAI enemyAI;
    void Start() 
    {
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            enemyAI.isAlerted = true;
        }
    }
}
