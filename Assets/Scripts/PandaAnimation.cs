using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PandaAnimation : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(SeeLune());
    }
    IEnumerator SeeLune()
    {
        animator.SetBool("SeenLune", true);
        yield return new WaitForSeconds(3f);
        animator.SetBool("SeenLune", false);
    }
}
