using UnityEngine;

public class GorilaEnemigo : MonoBehaviour
{
    public Transform player;
    public float velocidad = 4f;
    public float rangoDeteccion = 12f;
    public float rangoAtaque = 2.5f;
    
    private Animator anim;
    private Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
        if (player == null) 
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia < rangoDeteccion && distancia > rangoAtaque)
        {
            Perseguir();
        }
        else if (distancia <= rangoAtaque)
        {
            Atacar();
        }
        else
        {
            Detenerse();
        }
    }

    void Perseguir()
    {
        // 1. Rotar hacia el jugador
        Vector3 direccion = (player.position - transform.position).normalized;
        direccion.y = 0; 
        Quaternion rotacion = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 5f);

        // 2. Moverse físicamente
        rb.MovePosition(transform.position + transform.forward * velocidad * Time.deltaTime);
        
        // SUAVIZADO: En lugar de 1f de golpe, subimos a 1 suavemente
        float valorActual = anim.GetFloat("Velocidad");
        anim.SetFloat("Velocidad", Mathf.Lerp(valorActual, 1f, Time.deltaTime * 5f));
    }

    void Atacar()
    {
        // Cuando ataca, la velocidad debe ser 0 para que no camine mientras golpea
        float valorActual = anim.GetFloat("Velocidad");
        anim.SetFloat("Velocidad", Mathf.Lerp(valorActual, 0f, Time.deltaTime * 8f));
        
        // Solo lanzamos el trigger si no está ya en la animación de ataque
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("GorilaAtacar"))
        {
            anim.SetTrigger("Atacar");
        }
    }

    void Detenerse()
    {
        // Bajamos la velocidad a 0 suavemente para evitar el "tembleque"
        float valorActual = anim.GetFloat("Velocidad");
        anim.SetFloat("Velocidad", Mathf.Lerp(valorActual, 0f, Time.deltaTime * 5f));
    }
}