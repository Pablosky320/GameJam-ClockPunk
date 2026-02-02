using UnityEngine;

public class VidaGorila : MonoBehaviour
{
    public float vidaMaxima = 100f;
    private float vidaActual;
    private bool estaMuerto = false;
    private Animator anim;
    private Rigidbody rb;

    void Start()
    {
        vidaActual = vidaMaxima;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void RecibirDanioGorila(float d)
    {
        if (estaMuerto) return;
        vidaActual -= d;
        if (vidaActual <= 0) Morir();
    }

    void Morir()
    {
        estaMuerto = true;
        
        // 1. Trigger de muerte
        if (anim != null) anim.SetTrigger("Morir"); 

        // 2. Congelamos la física para que no se hunda
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true; 
        }

        // 3. DESACTIVAMOS TODO DAÑO (Las manos)
        // Buscamos todos los scripts de DañoEnemigo en los hijos y los apagamos
        DañoEnemigo[] manos = GetComponentsInChildren<DañoEnemigo>();
        foreach (DañoEnemigo mano in manos)
        {
            mano.enabled = false; // El script deja de funcionar
            if(mano.GetComponent<Collider>()) mano.GetComponent<Collider>().enabled = false; // El collider desaparece
        }

        // 4. Apagamos su collider principal y movimiento
        GetComponent<Collider>().enabled = false;
        MonoBehaviour mov = GetComponent("GorilaEnemigo") as MonoBehaviour;
        if (mov != null) mov.enabled = false;

        Destroy(gameObject, 5f); 
    }
}