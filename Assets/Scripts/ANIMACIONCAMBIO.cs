using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ANIMACIONCAMBIO : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Animation());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(2);
            StopAllCoroutines();
        }
    }
    IEnumerator Animation()
    {
        yield return new WaitForSeconds(58);
        SceneManager.LoadScene(2);
    }
}
