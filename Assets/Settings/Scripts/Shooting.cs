using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    int ammo;
    int maxAmmo = 6;
    float reloadTime = 1.2f;
    float firingSpeed = .4f;
    bool isFiring;

    void Start()
    {
        ammo = maxAmmo;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(ammo != 0)
            {
                if(!isFiring)
                {
                    StartCoroutine(Shoot());
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(ammo != 6)
            {
                StartCoroutine(Reload());
            }
        }
    }

    IEnumerator Shoot()
    {
        isFiring = true;
        Debug.Log("El personaje dispara");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        ammo = ammo - 1;
        yield return new WaitForSeconds(firingSpeed);
        isFiring = false;
    }
    
    IEnumerator Reload()
    {
        Debug.Log("El personaje empieza a recargar");
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        Debug.Log("El personaje termina de recargar");
    }
}
