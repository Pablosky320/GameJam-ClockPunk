using System.Collections;
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

    public bool isAlerted;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerPosition = player.gameObject.transform;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isAlerted == true)
        {
            Chase();
        }
    }

    void Chase()
    {
        //esto determina la distancia entre el enemigo y el jugador
        float distance = Vector3.Distance(transform.position, playerPosition.position);
        agent.SetDestination(playerPosition.position);

        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(playerPosition.position);
        }
        else
        {
            agent.isStopped = true; // Se para cuando esta cerca
            StartCoroutine(Attacking());
        }


    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(2);
    }
}
