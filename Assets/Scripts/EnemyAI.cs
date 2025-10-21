using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Windows;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;


    [SerializeField] bool isAlerted;
    


    private void Update()
    {
        playerDirection = new Vector3(player.transform.position.x, 0, player.transform.position.y);

        if (isAlerted == true)
        {
            Alerted();
        }
    }

    private void OnTriggerEnter(Collider Jugador)
    {
        isAlerted = true;
    }

    void Alerted()
    {
        age
    }
}
