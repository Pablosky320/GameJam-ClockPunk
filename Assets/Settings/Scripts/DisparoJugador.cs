using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntaPistola;
    public float velocidadBala = 30f;

    void Update()
    {
        // 1. Girar hacia el ratón
        MirarAlRaton();

        // 2. Disparar con click izquierdo
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }

    void MirarAlRaton()
    {
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit golpe;

        // Si el rayo toca cualquier objeto (como el suelo)
        if (Physics.Raycast(rayo, out golpe))
        {
            Vector3 puntoObjetivo = golpe.point;
            puntoObjetivo.y = transform.position.y; // Evita que el jugador se incline hacia arriba/abajo

            // Girar el cuerpo hacia el punto del ratón
            transform.LookAt(puntoObjetivo);
        }
    }

    void Disparar()
    {
        if (puntaPistola != null && balaPrefab != null)
        {
            GameObject nuevaBala = Instantiate(balaPrefab, puntaPistola.position, transform.rotation);
            Rigidbody rb = nuevaBala.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.linearVelocity = transform.forward * velocidadBala;
                rb.useGravity = false;
            }
            Destroy(nuevaBala, 2f);
        }
    }
}