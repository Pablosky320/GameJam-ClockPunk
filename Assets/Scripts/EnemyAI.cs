using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Windows;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform playerPosition;
    public GameObject player;
    public float stopDistance = 2f;

    [SerializeField] bool isAlerted;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerPosition = player.gameObject.transform;


    }

    private void Update()
    {
        //esto determina la distancia entre el enemigo y el jugador
        float distance = Vector3.Distance(transform.position, playerPosition.position);
        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(playerPosition.position);
        }
        else
        {
            agent.isStopped = true; // Stop moving when close enough
        }
    }

    private void FixedUpdate()
    {
        if (isAlerted == true)
        {
            Chase();    
        } 
    }

    private void OnTriggerEnter(Collider Jugador)
    {
        isAlerted = true;
        
    }

    void Chase()
    {
        agent.SetDestination(playerPosition.position);

    }
}
