using UnityEngine;

public class GorilaEnemigo : MonoBehaviour
{
    public Transform player;
    public float velocidad = 4f;
    public float rangoAtaque = 3.5f; // Rango aumentado para que el brazo llegue
    
    private Animator anim;
    private Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia > rangoAtaque) {
            Perseguir();
        } else {
            Atacar();
        }
    }

    void Perseguir()
    {
        Vector3 direccion = (player.position - transform.position).normalized;
        direccion.y = 0;
        if (direccion != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direccion), Time.deltaTime * 10f);
        }
        rb.MovePosition(transform.position + transform.forward * velocidad * Time.deltaTime);
        anim.SetFloat("Velocidad", 1f);
    }

    void Atacar()
    {
        anim.SetFloat("Velocidad", 0f);
        // Usamos el nombre exacto de tu Animator: "ataque"
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("ataque")) {
            anim.SetTrigger("Atacar");
        }
    }
}