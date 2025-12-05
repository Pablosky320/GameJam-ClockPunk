using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(hitEffect);
        Destroy(gameObject);
    }
}
