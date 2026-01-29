using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        // Hace que el objeto siempre mire hacia donde mira la cámara
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                         Camera.main.transform.rotation * Vector3.up);
    }
}