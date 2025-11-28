using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    GameObject hurtbox;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(Attack());
        }

    }

    IEnumerator Attack();
    {
        hurtbox.SetActive(false);



        yield return new WaitForSeconds;
    }
}
