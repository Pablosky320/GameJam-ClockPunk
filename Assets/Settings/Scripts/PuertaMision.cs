using UnityEngine;

public class PuertaMision : MonoBehaviour
{
    public int muertesNecesarias = 4;
    
    void Update()
    {
        // El script mira el contador global que creamos en SaludEnemigo
        // En cuanto detecta que es 4 o más, ejecuta la apertura
        if (SaludEnemigo.contadorMuertesGlobal >= muertesNecesarias)
        {
            AbrirPuerta();
        }
    }

    void AbrirPuerta()
    {
        // Opción 1: La puerta simplemente desaparece
        gameObject.SetActive(false); 
        
        // Opción 2 (Opcional): Si quieres que suba, usa esto en vez de lo anterior:
        // transform.Translate(Vector3.up * Time.deltaTime * 2f);
        
        Debug.Log("¡Puerta abierta!");
    }
}