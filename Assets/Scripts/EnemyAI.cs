using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Windows;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] bool isAlerted;
    public GameObject player;
    public Vector3 playerDirection;
    Rigidbody rb;
    float speed = 5f; 


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
        rb.MovePosition(transform.position + (transform.forward * playerDirection.magnitude) * speed * Time.deltaTime);
    }
}
