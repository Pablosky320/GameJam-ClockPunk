using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Usamos LateUpdate para que se ejecute después de que el enemigo se mueva
    void LateUpdate()
    {
        // Esto hace que el Canvas siempre mire hacia adelante como la cámara
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}