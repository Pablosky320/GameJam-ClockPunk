using UnityEngine;

public class TeleportPuerta : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("Teleport"))
                {
            //transform.position = new Vector3(-16f, 4, -6f);
            Debug.Log("funciona lampuerta");
                }
    }
}
