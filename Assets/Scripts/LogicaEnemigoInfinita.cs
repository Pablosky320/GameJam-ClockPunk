using UnityEngine;
using System.Collections;

public class LogicaEnemigoInfinita : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject balaPrefab;
    public Transform puntaPistola;

    [Header("Configuración")]
    public float tiempoDeRecarga = 1.08f;    
    public float velocidadBala = 25f;

    private Animator anim;
    private Transform jugador;
    private bool jugadorMuerto = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) jugador = playerObj.transform;

        StartCoroutine(CicloDeAtaque());
    }

    void Update()
    {
        // Si el jugador no existe o está muerto, el enemigo no hace nada
        if (jugador == null || jugadorMuerto) return;

        // Rotación hacia el jugador
        Vector3 direccion = (jugador.position - transform.position).normalized;
        direccion.y = 0; 
        if (direccion != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direccion);
        }
    }

    IEnumerator CicloDeAtaque()
    {
        while (!jugadorMuerto)
        {
            // FASE DE DISPARO: 3 VECES
            for (int i = 0; i < 3; i++)
            {
                // Asegúrate de que tu animación en el Animator se llame exactamente "Disparo"
                anim.Play("Disparo", -1, 0f);
                
                yield return new WaitForSeconds(1.0f); // Tiempo hasta que sale la bala
                
                InstanciarBala();
                
                yield return new WaitForSeconds(1.0f); // Tiempo para bajar el arma
            }

            // FASE DE RECARGA
            anim.Play("Recarga", -1, 0f);
            yield return new WaitForSeconds(tiempoDeRecarga);
        }
    }

    void InstanciarBala()
    {
        if (balaPrefab != null && puntaPistola != null)
        {
            GameObject b = Instantiate(balaPrefab, puntaPistola.position, puntaPistola.rotation);
            Rigidbody rbBala = b.GetComponent<Rigidbody>();
            if (rbBala != null) rbBala.linearVelocity = puntaPistola.forward * velocidadBala;
            
            // Le metemos el script de daño a la bala si no lo tiene
            if (!b.GetComponent<BalaEnemigoDaño>()) {
                b.AddComponent<BalaEnemigoDaño>();
            }
        }
    }

    // Función para que el Player le diga al enemigo que pare
    public void PararAtaque() {
        jugadorMuerto = true;
        StopAllCoroutines();
    }
}